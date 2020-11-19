using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementType
{
    protected float Speed;

    public MovementType()
    {
        Speed = 0f;
    }

    public virtual void SetSpeed(float speed)
    {
        this.Speed = speed;
    }
    
    public abstract void Move(Vector3 direction, ref Transform objectTransform, float fixedDeltaTime);
}
