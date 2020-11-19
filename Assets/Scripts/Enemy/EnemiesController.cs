using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemiesController : MonoBehaviour
{

    [SerializeField]private List<Transform> _allEnemies;

    public event Action OnStopGame = () => { };

    public void Init()
    {
        _allEnemies = new List<Transform>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            _allEnemies.Add(objs[i].transform);
        }
    }

    public void RefreshList()
    {
        _allEnemies = _allEnemies.Where(item => item != null).ToList();
    }

    public void StopAllEnemies()
    {
        OnStopGame();
    }

    public List<Transform> GetAllEnemies()
    {
        return _allEnemies;
    }

    public void Remove(Transform e)
    {
        _allEnemies.Remove(e);
    }
}
