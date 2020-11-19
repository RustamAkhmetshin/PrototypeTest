using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : Weapon
{
    private float _shootTime = 0f;
    private IPool _bulletPool;
    
    
    public StandardWeapon(IPool pool)
    {
        BulletPlace = new RectTransform();
        _bulletPool = pool;
    }
    
    public override void SetBulletPlace(Transform bulletPlace)
    {
        BulletPlace = bulletPlace;
    }

    public override void Shoot(float deltaTime)
    {
        _shootTime += deltaTime;
        if (_shootTime >= ShootLatency)
        {
            _bulletPool.Spawn(BulletPlace.position).Init(BulletSpeed, DamageStrength, BulletPlace.rotation);
            _shootTime = 0;
        }
    }
    
}
