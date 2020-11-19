using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    private float _shootTime = 0f;
    private float _shootingTime = 0f;
    private IPool _bulletPool;
    
    
    public EnemyWeapon(Transform bulletPlace, IPool pool)
    {
        BulletPlace = bulletPlace;
        _bulletPool = pool;
    }
    
    public override void SetBulletPlace(Transform bulletPlace)
    {
        BulletPlace = bulletPlace;
    }

    public override void Shoot(float deltaTime)
    {
        _shootingTime += deltaTime;
        _shootTime += deltaTime;

        if (_shootTime >= ShootLatency)
        {
            Shoot(deltaTime);
            _shootTime = 0;
        }

        if (_shootingTime > ShootingTime)
        {
            EventBus.RaiseEvent<IEnemyStateControlHandler>(h => h.ChangeEnemyState(CharacterUnit.UnitState.IsMoving));
            _shootingTime = 0;
            
        }
    }
}
