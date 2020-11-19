using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    public void Init(float speed, float damage, Quaternion rotation)
    {
        this._speed = speed;
        this._damage = damage;
        transform.rotation = rotation;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed);    
    }

    public float Damage => _damage;

    private void OnTriggerEnter(Collider collider)
    {
        var bulletPool = Root.Pool;
        bulletPool.HideToPool(this);
    }
}
