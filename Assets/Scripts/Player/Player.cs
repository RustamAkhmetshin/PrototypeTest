using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterUnit
{
    public Vector3 moveDirection;
    
    private float _shootTime = 0;
   
    
    public Player(float maxHealth, float moveSpeed, float shootLatency, float bulletSpeed, float damageStrength, Transform characterTransform) 
        : base(maxHealth, moveSpeed, shootLatency, bulletSpeed, damageStrength, characterTransform)
    {
        this.moveDirection = Vector3.zero;
    }
    
    public event Action OnDied = () => { };
    public event Action<Transform> OnReadyToShoot = (target) => { };

    public override void Move(Vector3 direction)
    {
        moveDirection = direction;
        CurrentState = State.IsMoving;
    }

    public override void Update(float deltaTime)
    {
        _shootTime += deltaTime;
        
        if (CurrentState == State.IsShooting)
        {
            if (_shootTime >= ShootLatency)
            {
                Shoot();
                _shootTime = 0;
            }    
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentState == State.IsMoving)
        {
            CharacterTransform.Translate(Vector3.forward * MoveSpeed);
            CharacterTransform.rotation = Quaternion.LookRotation(moveDirection);
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

    public Vector3 GetPosition()
    {
        return CharacterTransform.position;
    }
    
    public Quaternion GetRotation()
    {
        return CharacterTransform.rotation;
    }

    public void StartShoting()
    {
        CurrentState = State.IsShooting;
    }
    
    public void StopShoting()
    {
        CurrentState = State.Idle;
    }

    public override void Die()
    {
        CurrentState = State.IsNotAlive;
        OnDied();
    }

    public override void Shoot()
    {
        List<Transform> enemies = Root.EnemiesController.GetAllEnemies();
        if (enemies != null)
        {
            List<Transform> visibleEnemies = new List<Transform>();
            Transform target = null;

            foreach (var e in enemies)
            {
                if(e == null) return;
                if (!Physics.Linecast(CharacterTransform.position, e.position))
                {
                    visibleEnemies.Add(e);
                }
            }

            target = FindMinimalDistanceEnemy(visibleEnemies);

            if (target == null)
            {
                target = FindMinimalDistanceEnemy(enemies);
            }

            OnReadyToShoot(target);
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        Bullet b = collider.GetComponent<Bullet>();
        if (b != null)
        {
            AddDamage(b.Damage);
        }
    }

    private Transform FindMinimalDistanceEnemy(List<Transform> enemies)
    {
        if (enemies.Count == 0) 
            return null;
        
        float minDistance = Vector3.Distance(CharacterTransform.position, enemies[0].position);
        Transform minimalByDistance = enemies[0];
        for (int i = 1; i < enemies.Count; i++)
        {
            float tmpDistance = Vector3.Distance(CharacterTransform.position, enemies[i].position);
            if (tmpDistance < minDistance)
            {
                minDistance = tmpDistance;
                minimalByDistance = enemies[i];
            }
        }

        return minimalByDistance;
    }
}
