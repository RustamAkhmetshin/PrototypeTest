
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemiesController
{
    
    event Action OnStopGame;
    void Init();
    void StopAllEnemies();
    List<Transform> GetAllEnemies();
    void Remove(Transform e);
}
