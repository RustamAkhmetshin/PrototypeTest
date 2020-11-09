using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    [SerializeField] private GameObject _restartWindow;
    public GameObject GetRestartWindow()
    {
        return _restartWindow;
    }
}
