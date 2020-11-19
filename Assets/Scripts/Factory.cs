using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameObject _uiManager;
    [SerializeField] private GameObject _pool;
    [SerializeField] private GameObject _levelManager;
    [SerializeField] private GameObject _inputManager;
    [SerializeField] private GameObject _dataManager;
    
    public IUIManager GetUIManager()
    {
        return Instantiate(_uiManager, transform).GetComponent<IUIManager>();
    }

    public IPool GetPool()
    {
        return Instantiate(_pool, transform).GetComponent<IPool>();
    }
    
    public ILevelManager GetLevelManager()
    {
        return Instantiate(_levelManager, transform).GetComponent<ILevelManager>();
    }
    
    public IInputManager GetInputManager()
    {
        return Instantiate(_inputManager, transform).GetComponent<IInputManager>();
    }

    public IDataManager GetDataManager()
    {
        return Instantiate(_dataManager, transform).GetComponent<IDataManager>();
    }
}
