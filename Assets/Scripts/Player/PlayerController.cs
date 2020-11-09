using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController, IShoot, IDamagable
{
    [SerializeField] private GameObject _joystickGameObject;
    [SerializeField] private float _speed;
    [SerializeField] private float _shootLatency;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private Transform _bulletPlace;
    
    
    private IPool _bulletPool => Root.BulletPool;
    private bool _isShooting;
    private bool _isMoving;
    private Vector3 _moveDirection;
    private float _health;
    private bool _isAlive;
    

    public void Init()
    {
        _moveDirection = Vector3.zero;
        _isMoving = false;
        _isShooting = false;
        _health = 100f;
        _isAlive = true;
        
        var joystick = Root.UIManager.InitializeWindow<JoystickUIComponent>(_joystickGameObject,
            Root.UIManager.GetMainCanvas().transform);

        joystick.OnMove += Move;
        joystick.OnTouchDown += StopShoting;
        joystick.OnTouchUp += StartShoting;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.Translate(Vector3.forward * _speed);
            transform.rotation = Quaternion.LookRotation(_moveDirection);
        }
    }

    private float _shootTime = 0;

    private void Update()
    {
        _shootTime += Time.deltaTime;
        
        if (_isShooting)
        {
            if (_shootTime >= _shootLatency)
            {
                Shoot();
                _shootTime = 0;
            }    
        }
    }
    
    public void Move(Vector3 vector)
    {
        _isMoving = true;
        _moveDirection = vector;
    }

    public void StartShoting()
    {
        _isMoving = false;
        _isShooting = true;
    }

    public void StopShoting()
    {
        _isShooting = false;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void Die()
    {
        StopShoting();
        _isMoving = false;
        _isAlive = false;
        Root.UIManager.HideWindow("JoystickUIComponent");
        //GameManager - GameOver\Restart;
        
        
        Root.UIManager.InitializeWindow<RestartWindow>(Root.LevelManager.GetRestartWindow(),
            Root.UIManager.GetMainCanvas().transform);
    }

    public void Shoot()
    {
        List<Transform> enemies = Root.EnemiesController.GetAllEnemies();
        List<Transform> visibleEnemies = new List<Transform>();
        Transform target = null;

        foreach (var e in enemies)
        {
            if (!Physics.Linecast(transform.position, e.position))
            {
                visibleEnemies.Add(e);
            }
        }

        target = FindMinimalDistanceEnemy(visibleEnemies);

        if (target == null)
        {
            target = FindMinimalDistanceEnemy(enemies);
        }

        if (target)
        {
            transform.LookAt(target);
            _bulletPool.Spawn(_bulletPlace.position).Init(_bulletSpeed, _bulletDamage, transform.rotation);
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

    public void AddDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0 && _isAlive)
        {
            Die();
        }
    }

    private Transform FindMinimalDistanceEnemy(List<Transform> enemies)
    {
        if (enemies.Count == 0) 
            return null;
        
        float minDistance = Vector3.Distance(transform.position, enemies[0].position);
        Transform minimalByDistance = enemies[0];
        for (int i = 1; i < enemies.Count; i++)
        {
            float tmpDistance = Vector3.Distance(transform.position, enemies[i].position);
            if (tmpDistance < minDistance)
            {
                minDistance = tmpDistance;
                minimalByDistance = enemies[i];
            }
        }

        return minimalByDistance;
    }
    
}
