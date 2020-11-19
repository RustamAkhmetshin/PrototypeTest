using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyTypes _type;
    [SerializeField] private Transform _bulletPlace;
    [SerializeField] private List<Transform> _movePoints;
    
    private Enemy _enemy;

    private bool _initialized = false;
    
    
    public void Init(EnemyData enemyData, MovementType movementType, Weapon weapon)
    {
        var data = Root.DataManager.GetEnemiesData()._enemyTypes.FirstOrDefault(e => e.Name == _type.ToString());
        _enemy = new Enemy(data, movementType, weapon, transform);
        
        _enemy.OnDied += DieEventHandler;

        _initialized = true;
        _enemy.Move(new Vector3());
    }
    
    private void Update()
    {
        if (_initialized)
        {
            _enemy.Update(Time.deltaTime);
            transform.rotation = _enemy.Rotation;
        }
    }

    private void FixedUpdate()
    {
        if (_initialized)
        {
            _enemy.FixedUpdate(Time.fixedDeltaTime);
            transform.position = GetPlayerPosition();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _enemy.Hit(collider);
    }
    
    public Vector3 GetPlayerPosition()
    {
        return _enemy.Position;
    }

    private void DieEventHandler()
    {
        EventBus.RaiseEvent<IEnemyControlHandler>(h => h.EnemyKilled(transform));
        Destroy(gameObject);
    }
    
}
