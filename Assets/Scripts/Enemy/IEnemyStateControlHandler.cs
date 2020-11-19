using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public interface IEnemyStateControlHandler : IGlobalSubscriber
{
    void ChangeEnemyState(CharacterUnit.UnitState state);
}
