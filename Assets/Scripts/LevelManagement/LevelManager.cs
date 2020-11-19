using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager, IEnemyControlHandler
{
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject _restartWindow;

    private List<Transform> _spawnedEnemies;
    

    private bool _gameStarted = false;

    
    public void Start()
    {
        _spawnedEnemies = new List<Transform>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            _spawnedEnemies.Add(objs[i].transform);
        }
        
        EventBus.Subscribe(this);
    }

    public void RefreshList()
    {
        _spawnedEnemies = _spawnedEnemies.Where(item => item != null).ToList();
    }
    
    public bool IsGameStarted()
    {
        return _gameStarted;
    }

    public void StartGame()
    {
        Root.Pool.Preload(20);

        var playerData = Root.DataManager.GetPlayerData();
        MovementType m = new StandardPlayerMovement();
        Weapon w = new StandardWeapon(Root.Pool);
        
        Instantiate(_playerController).GetComponent<PlayerController>().Init(playerData, m, w, Root.InputManager);
        
        _gameStarted = true;
    }

    public GameObject GetRestartWindow()
    {
        return _restartWindow; 
    }

    public void GameOver()
    {
        
        Root.UIManager.HideWindow("JoystickUIComponent");
        
        Root.UIManager.InitializeWindow<RestartWindow>(Root.LevelManager.GetRestartWindow(),
            Root.UIManager.GetMainCanvas().transform);
    }

    public List<Transform> GetSpawnedEnemiesList()
    {
        return _spawnedEnemies;
    }

    public void EnemyKilled(Transform t)
    {
        _spawnedEnemies.Remove(t);
        RefreshList();
    }
}
