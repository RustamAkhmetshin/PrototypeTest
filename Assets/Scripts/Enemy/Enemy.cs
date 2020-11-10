using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterUnit
{
    private float _shootingTime;
    private float _shootTime = 0;

    private List<Vector3> _movePoints;
    private Vector3 _targetToMove;
    private int _targetToMoveIndex;

    private float ShootingTime;

    public Enemy(float maxHealth, float moveSpeed, float shootLatency, float bulletSpeed, float damageStrength, Transform characterTransform, float shootingTime, List<Transform> movePoints)
        : base(maxHealth, moveSpeed, shootLatency, bulletSpeed, damageStrength, characterTransform)
    {
        this.ShootingTime = shootingTime;
        this._movePoints = new List<Vector3>();
        foreach (var mp in movePoints)
        {
            this._movePoints.Add(mp.position);
        }
        _targetToMove = _movePoints[0];
        _targetToMoveIndex = 0;
    }
    
    public event Action OnDied = () => { };
    public event Action<Transform> OnReadyToShoot = (target) => { };
    
    public override void Move(Vector3 direction)
    {
        CurrentState = State.IsMoving;
    }

    public override void Update(float deltaTime)
    {
        if (CurrentState == State.IsNotAlive)
        {
            return;
        }
        
        if (CurrentState == State.IsShooting)
        {
            _shootingTime += deltaTime;
            _shootTime += deltaTime;

            if (_shootTime >= ShootLatency)
            {
                Shoot();
                _shootTime = 0;
            }

            if (_shootingTime > ShootingTime)
            {
                if (CurrentState != State.IsNotAlive)
                {
                    CurrentState = State.IsMoving;
                    _shootingTime = 0;
                }
            }
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentState == State.IsNotAlive)
        {
            return;
        }
        
        if (CurrentState == State.IsMoving)
        {
            float step =  MoveSpeed * fixedDeltaTime;
            CharacterTransform.position = Vector3.MoveTowards(CharacterTransform.position, _targetToMove, step);
            if (Vector3.Distance(CharacterTransform.position, _targetToMove) < 0.001f)
            {
                
                _targetToMoveIndex = (_targetToMoveIndex + 1) % 2;
                _targetToMove = _movePoints[_targetToMoveIndex];
                if(CurrentState != State.IsNotAlive)
                    CurrentState = State.IsShooting;
            }
        }
    }

    public void StopGame()
    {
        CurrentState = State.IsNotAlive;
    }
    

    public override void Hit(Collider collider)
    {
        Bullet b = collider.GetComponent<Bullet>();
        if (b != null)
        {
            AddDamage(b.Damage);
        }
    }
    
    public override void Die()
    {
        CurrentState = State.IsNotAlive;
        OnDied();
    }
    
    public Vector3 GetPosition()
    {
        return CharacterTransform.position;
    }
    
    public override void Shoot()
    {
        OnReadyToShoot(CharacterTransform);
    }
}
