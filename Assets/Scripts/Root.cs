using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    private static Root _instance;
    private static Factory _factory;
    private GameMode _currentGameMode;
    private bool _initialized;
    
    private IUIManager _uiManager;
    private IJoystickUIComponent _joystickUiComponent;
    private IPlayerController _playerController;
    private IPool _bulletPool;
    private ILevelManager _levelManager;
    private IEnemiesController _enemiesController;

    void Awake()
    {
        _factory = GetComponent<Factory>();
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance == this)
        {
            Destroy(gameObject);
        }

       // DontDestroyOnLoad(gameObject);
        
        Initialize();
    }

    public void Initialize()
    {
        BulletPool.Preload(20);
        PlayerController.Init();
        _initialized = true;
    }
    
    public static IUIManager UIManager
    {
        get { return _instance._uiManager = _instance._uiManager ?? _factory.GetUIManager(); }
    }
    
    public static IJoystickUIComponent JoystickUiComponent
    {
        get { return _instance._joystickUiComponent = _instance._joystickUiComponent ?? _factory.GetJoystickUIComponent(); }
    }
    
    public static IPlayerController PlayerController
    {
        get { return _instance._playerController = _instance._playerController ?? _factory.GetPlayerController(); }
    }
    
    public static IPool BulletPool
    {
        get { return _instance._bulletPool = _instance._bulletPool ?? _factory.GetBulletPool(); }
    }
    
    public static ILevelManager LevelManager
    {
        get { return _instance._levelManager = _instance._levelManager ?? _factory.GetLevelManager(); }
    }
    
    public static IEnemiesController EnemiesController
    {
        get { return _instance._enemiesController = _instance._enemiesController ?? _factory.GetEnemiesController(); }
    }
}
