using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "WeaponData", menuName = "WeaponData" )]
public class WeaponData : ScriptableObject
{
    public string Name;
    public float ShootLatency;
    public float ShootingTime;
    public float BulletSpeed;
    public float DamageStrength;
}
