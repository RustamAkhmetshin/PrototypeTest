
using System.Collections.Generic;
using UnityEngine;

public interface IEnemiesController
{
    List<Transform> GetAllEnemies();
    void Remove(Transform e);
}
