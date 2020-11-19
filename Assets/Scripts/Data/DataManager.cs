using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IDataManager
{
    [SerializeField] private PlayerConfiguration _playerConfiguration;
    [SerializeField] private EnemiesData _enemiesData;
    [SerializeField] private GlobalVariables _globalVariables;
    
    public PlayerConfiguration GetPlayerData()
    {
        return _playerConfiguration;
    }

    public EnemiesData GetEnemiesData()
    {
        return _enemiesData;
    }

    public GlobalVariables GetGlobalVariables()
    {
        return _globalVariables;
    }
}
