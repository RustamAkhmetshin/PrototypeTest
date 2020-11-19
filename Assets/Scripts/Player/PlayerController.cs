using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IInputManager _inputManager;
    [SerializeField] private Transform _bulletPlace;
    
    private Player _player;
    private IPool _bulletPool => Root.Pool;
    
    private bool _initialized = false;

    public void Init(PlayerConfiguration playerConfig, MovementType movementType, Weapon weapon, IInputManager inputManager)
    {
        weapon.SetBulletPlace(_bulletPlace);
        _player = new Player(playerConfig,  movementType, weapon, transform);
        _player.OnDied += DieEventHandler;

        _inputManager = inputManager;
        _inputManager.InitInputManager();

        _inputManager.OnMove += Move;
        _inputManager.OnStartShooting += StartShoting;
        _inputManager.OnStopShooting += StopShoting;
        
        
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
        return _player.Position;
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }

    public Quaternion GetPlayerRotation()
    {
        return _player.Rotation;
    }
    
    public void Move(Vector3 vector)
    {
        Debug.Log(vector);
        _player.Move(vector);
    }

    public void StartShoting()
    {
        Debug.Log("123123");
        _player.StartShoting();
    }
    
    private void DieEventHandler()
    {
        Root.LevelManager.GameOver();
        Destroy(gameObject);
    }
}
