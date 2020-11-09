using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameObject _uiManager;
    [SerializeField] private GameObject _joystickUIComponent;
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject _bulletPool;
    [SerializeField] private GameObject _levelManager;
    [SerializeField] private GameObject _enemiesController;
    
    public IUIManager GetUIManager()
    {
        return Instantiate(_uiManager, transform).GetComponent<IUIManager>();
    }
    
    public IJoystickUIComponent GetJoystickUIComponent()
    {
        return Instantiate(_joystickUIComponent, transform).GetComponent<IJoystickUIComponent>();
    }
    
    public IPlayerController GetPlayerController()
    {
        return Instantiate(_playerController, transform).GetComponent<IPlayerController>();
    }
    
    public IPool GetBulletPool()
    {
        return Instantiate(_bulletPool, transform).GetComponent<IPool>();
    }
    
    public ILevelManager GetLevelManager()
    {
        return Instantiate(_levelManager, transform).GetComponent<ILevelManager>();
    }
    
    public IEnemiesController GetEnemiesController()
    {
        return Instantiate(_enemiesController, transform).GetComponent<IEnemiesController>();
    }
}
