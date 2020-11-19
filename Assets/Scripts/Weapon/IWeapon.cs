using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected float ShootLatency;
    protected float ShootingTime;
    protected float BulletSpeed;
    protected float DamageStrength;
    
    protected Transform BulletPlace;

    public abstract void SetBulletPlace(Transform bulletPlace);
    public abstract void Shoot(float deltaTime);
}
