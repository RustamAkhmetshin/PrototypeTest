using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour, IShoot, IDamagable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _shootLatency;
    [SerializeField] private Transform _bulletPlace;

    private IPlayerController _player => Root.PlayerController;
    private IPool _bulletPool => Root.BulletPool;

    [SerializeField] private List<Transform> _movePoints;

    private float _currentHealth;

    private bool _isAlive;
    
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;
        Move();
    }

    private float _shootTime = 0;

    private void Update()
    {
        if (_isAlive)
        {
            _shootTime += Time.deltaTime;

            if (_shootTime >= _shootLatency)
            {
                Shoot();
                _shootTime = 0;
            }
        }
    }

    public virtual void Move()
    {
        StartCoroutine("TransitPositionCoroutine");
    }

    public virtual void Shoot()
    {
        transform.LookAt(_player.GetPlayerPosition());
        _bulletPool.Spawn(_bulletPlace.position).Init(_bulletSpeed, _bulletDamage, transform.rotation);

    }

    private void OnTriggerEnter(Collider collider)
    {
        Bullet b = collider.GetComponent<Bullet>();
        if (b != null)
        {
            AddDamage(b.Damage);
        }
    }
    

    public virtual void AddDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        _isAlive = false;
        Root.EnemiesController.Remove(transform);
        Destroy(gameObject);
    }

    private IEnumerator TransitPositionCoroutine()
    {
        Vector3 point_0 = _movePoints[0].position;
        Vector3 point_1 = _movePoints[1].position;
        while (_isAlive)
        {
            Vector3 from = transform.position;
            yield return new WaitForSeconds(1f);
            
            for(var t = 0f; t < 1; t += Time.deltaTime/_moveSpeed)
            {
                transform.position = Vector3.Lerp(from, point_1, t);
                yield return null;
            }
            transform.position = point_1;
            from = point_1;
            
            yield return new WaitForSeconds(1f);
            
            for(var t = 0f; t < 1; t += Time.deltaTime/_moveSpeed)
            {
                transform.position = Vector3.Lerp(from, point_0, t);
                yield return null;
            }
            transform.position =point_0;
        }
    }
}
