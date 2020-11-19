using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class StandardPlayerMovement : MovementType
{
    
    public override void Move(Vector3 direction, ref Transform objectTransform, float fixedDeltaTime)
    {
        objectTransform.Translate(Vector3.forward * Speed);
        objectTransform.rotation = Quaternion.LookRotation(direction);
    }
}
