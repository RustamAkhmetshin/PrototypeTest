using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemiesController : MonoBehaviour, IEnemiesController
{
    //класс-заглушка

    //Жесткая привязка к объектам врагов на сцене временная (спаун врагов и генерация уровней может 
    //быть реализована разными способами, так как пока нет конкретного менеджера уровней и адаптации к левел-дизайну, оставил так.
    
    public List<Transform> _allEnemies;
    
    public List<Transform> GetAllEnemies()
    {
        return _allEnemies;
    }

    public void Remove(Transform e)
    {
        _allEnemies.Remove(e);
    }
}
