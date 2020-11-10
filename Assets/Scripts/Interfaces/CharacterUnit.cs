
using System;
using System.Data.Common;
using UnityEngine;

public abstract class CharacterUnit : IDamagable, IShoot
{
    protected float MaxHealth;
    protected float MoveSpeed;
    protected float CurrentHealth;
    protected float ShootLatency;
    protected Transform CharacterTransform;
    
    public float BulletSpeed;
    public float DamageStrength;
    
    protected enum State
    {
        IsMoving,
        IsShooting,
        IsNotAlive,
        Idle,
    }

    protected State CurrentState;

    public CharacterUnit(float maxHealth, float moveSpeed, float shootLatency, float bulletSpeed, float damageStrength, Transform characterTransform)
    {
        this.MaxHealth = maxHealth;
        this.MoveSpeed = moveSpeed;
        this.ShootLatency = shootLatency;
        this.BulletSpeed = bulletSpeed;
        this.DamageStrength = damageStrength;
        this.CharacterTransform = characterTransform;
        this.CurrentHealth = maxHealth;
        CurrentState = State.Idle;
    }

    public virtual void AddDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public abstract void Move(Vector3 direction);
    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate(float fixedDeltaTime);
    public abstract void Hit(Collider collider);

    public virtual void Die()
    {
        CurrentState = State.IsNotAlive;
    }

    public virtual void Shoot()
    {
        CurrentState = State.IsShooting;
    }
}
