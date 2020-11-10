using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    [SerializeField] private GameObject _restartWindow;

    private bool _gameStarted = false;

    public bool IsGameStarted()
    {
        return _gameStarted;
    }

    public void StartGame()
    {
        Root.BulletPool.Preload(20);
        Root.PlayerController.Init();
        Root.EnemiesController.Init();
        _gameStarted = true;
    }

    public GameObject GetRestartWindow()
    {
        return _restartWindow;
    }

    public void GameOver()
    {
        Root.EnemiesController.StopAllEnemies();
        
        Root.UIManager.HideWindow("JoystickUIComponent");
        
        Root.UIManager.InitializeWindow<RestartWindow>(Root.LevelManager.GetRestartWindow(),
            Root.UIManager.GetMainCanvas().transform);
    }
}
