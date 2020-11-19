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
    private IPool _pool;
    private ILevelManager _levelManager;
    private IInputManager _inputManager;
    private IDataManager _dataManager;

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
        LevelManager.StartGame();
        _initialized = true;
    }
    
    public static IUIManager UIManager
    {
        get { return _instance._uiManager = _instance._uiManager ?? _factory.GetUIManager(); }
    }
    
    public static IInputManager InputManager
    {
        get { return _instance._inputManager = _instance._inputManager ?? _factory.GetInputManager(); }
    }

    public static IPool Pool
    {
        get { return _instance._pool = _instance._pool ?? _factory.GetPool(); }
    }
    
    public static ILevelManager LevelManager
    {
        get { return _instance._levelManager = _instance._levelManager ?? _factory.GetLevelManager(); }
    }

    public static IDataManager DataManager
    {
        get { return _instance._dataManager = _instance._dataManager ?? _factory.GetDataManager(); }
    }
}
