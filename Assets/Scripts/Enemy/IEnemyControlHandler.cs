using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public interface IEnemyControlHandler : IGlobalSubscriber
{
    void EnemyKilled(Transform t);
}
