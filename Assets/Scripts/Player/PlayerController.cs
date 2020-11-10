using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private GameObject _joystickGameObject;
    [SerializeField] private Transform _bulletPlace;
    
    private Player _player;
    private IPool _bulletPool => Root.BulletPool;
    
    private bool _initialized = false;

    public void Init()
    {
        var data = Root.DataManager.GetPlayerData();
        
        _player = new Player(data.MaxHealth, data.MoveSpeed, data.ShootLatency, data.BulletSpeed, data.DamageStrength, transform);
        _player.OnDied += DieEventHandler;
        _player.OnReadyToShoot += ReadyToSpawnBullet;
        
        var joystick = Root.UIManager.InitializeWindow<JoystickUIComponent>(_joystickGameObject,
            Root.UIManager.GetMainCanvas().transform);

        joystick.OnMove += Move;
        joystick.OnTouchUp += StartShoting;

        _initialized = true;
    }

    private void Update()
    {
        if (_initialized)
        {
            _player.Update(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_initialized)
        {
            _player.FixedUpdate(Time.fixedDeltaTime);
            transform.position = GetPlayerPosition();
            transform.rotation = GetPlayerRotation();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _player.Hit(collider);
    }

    public void StopShoting()
    {
        _player.StopShoting();
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.GetPosition();
    }

    public Transform GetPlayerTranssform()
    {
        return transform;
    }

    public Quaternion GetPlayerRotation()
    {
        return _player.GetRotation();
    }
    
    public void Move(Vector3 vector)
    {
        _player.Move(vector);
    }

    public void StartShoting()
    {
        _player.StartShoting();
    }
    
    private void DieEventHandler()
    {
        Root.LevelManager.GameOver();
        Destroy(gameObject);
    }

    private void ReadyToSpawnBullet(Transform target)
    {
        transform.LookAt(target);
        _bulletPool.Spawn(_bulletPlace.position).Init(_player.BulletSpeed, _player.DamageStrength, transform.rotation);
    }
}
