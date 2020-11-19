using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class StandardEnemyMovement : MovementType
{
    public List<Vector3> _movePoints;

    private Vector3 _targetToMove;
    private int _targetToMoveIndex = 0;
    
    public override void Move(Vector3 direction, ref Transform objectTransform, float fixedDeltaTime)
    {
        float step =  Speed * fixedDeltaTime;
        objectTransform.position = Vector3.MoveTowards(objectTransform.position, _targetToMove, step);
        if (Vector3.Distance(objectTransform.position, _targetToMove) < 0.001f)
        {
                
            _targetToMoveIndex = (_targetToMoveIndex + 1) % 2;
            _targetToMove = _movePoints[_targetToMoveIndex];
            
            EventBus.RaiseEvent<IEnemyStateControlHandler>(h => h.ChangeEnemyState(CharacterUnit.UnitState.IsShooting));
        }
    }
}
