using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterUnit, IDamagable
{
    private MovementType _movementType;
    private Weapon _weapon;
    private Transform _characterTransform;
    private Vector3 moveDirection;
    
    public Vector3 Position
    {
        get { return _characterTransform.position; }
    }
    
    public Quaternion Rotation
    {
        get { return _characterTransform.rotation; }
    }

    public Player(PlayerConfiguration playerConfig, MovementType movementType, Weapon weapon, Transform playerController) 
        : base(playerConfig.MaxHealth)
    {
        this.moveDirection = Vector3.zero;
        this._movementType = movementType;
        this._weapon = weapon;
        this._characterTransform = playerController;
        _movementType.SetSpeed(playerConfig.MoveSpeed);
    }
    
    public event Action OnDied = () => { };

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
        CurrentUnitState = UnitState.IsMoving;
    }

    public override void Update(float deltaTime)
    {
        if (CurrentUnitState == UnitState.IsShooting)
        {
            LookAtTargetEnemy();
            _weapon.Shoot(deltaTime);
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentUnitState == UnitState.IsMoving)
        {
            _movementType.Move(moveDirection, ref _characterTransform, fixedDeltaTime);
        }
    }

    public override void Hit(Collider collider)
    {
        Bullet b = collider.GetComponent<Bullet>();
        if (b != null)
        {
            AddDamage(b.Damage);
        }
    }
    
    public void StartShoting()
    {
        CurrentUnitState = UnitState.IsShooting;
    }
    
    public void StopShoting()
    {
        CurrentUnitState = UnitState.Idle;
    }

    public void AddDamage(float damage)
    {
        StateData.Health -= damage;
    }

    public void Die()
    {
        CurrentUnitState = UnitState.IsNotAlive;
        OnDied();
    }

    public void LookAtTargetEnemy()
    {
        List<Transform> enemies = Root.LevelManager.GetSpawnedEnemiesList();
        if (enemies != null)
        {
            List<Transform> visibleEnemies = new List<Transform>();
            Transform target = null;

            foreach (var e in enemies)
            {
                if(e == null) return;
                if (!Physics.Linecast(_characterTransform.position, e.position))
                {
                    visibleEnemies.Add(e);
                }
            }

            target = FindMinimalDistanceEnemy(visibleEnemies);

            if (target == null)
            {
                target = FindMinimalDistanceEnemy(enemies);
            }
        }
        
        
    }


    //TODO static extension method
    
    private Transform FindMinimalDistanceEnemy(List<Transform> enemies)
    {
        if (enemies.Count == 0) 
            return null;
        
        float minDistance = Vector3.Distance(_characterTransform.position, enemies[0].position);
        Transform minimalByDistance = enemies[0];
        for (int i = 1; i < enemies.Count; i++)
        {
            float tmpDistance = Vector3.Distance(_characterTransform.position, enemies[i].position);
            if (tmpDistance < minDistance)
            {
                minDistance = tmpDistance;
                minimalByDistance = enemies[i];
            }
        }

        return minimalByDistance;
    }
}
