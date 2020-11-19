using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public class Enemy : CharacterUnit, IDamagable, IEnemyStateControlHandler
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

    public Enemy(EnemyData enemyData, MovementType movementType, Weapon weapon, Transform enemyController)
        : base(enemyData.MaxHealth)
    {
        this.moveDirection = Vector3.zero;
        this._movementType = movementType;
        this._weapon = weapon;
        this._characterTransform = enemyController;
        _movementType.SetSpeed(enemyData.MoveSpeed);
        
        EventBus.Subscribe(this);
    }
    
    public event Action OnDied = () => { };
    public event Action<Transform> OnReadyToShoot = (target) => { };
    
    public void Move(Vector3 direction)
    {
        moveDirection = direction;
        CurrentUnitState = UnitState.IsMoving;
    }
    
    public void StartShoting()
    {
        CurrentUnitState = UnitState.IsShooting;
    }
    
    public void StopShoting()
    {
        CurrentUnitState = UnitState.Idle;
    }

    public override void Update(float deltaTime)
    {
        if (CurrentUnitState == UnitState.IsNotAlive)
        {
            return;
        }
        
        if (CurrentUnitState == UnitState.IsShooting)
        {
            LookAtPlayer();
           _weapon.Shoot(deltaTime);
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentUnitState == UnitState.IsNotAlive)
        {
            return;
        }
        
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

    public void AddDamage(float damage)
    {
        StateData.Health -= damage;
    }

    public void Die()
    {
        CurrentUnitState = UnitState.IsNotAlive;
        OnDied();
    }

    public void LookAtPlayer()
    {
        
    }

    public void ChangeEnemyState(UnitState state)
    {
        
    }
}
