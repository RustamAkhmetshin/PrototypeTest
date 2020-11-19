using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUIComponent : Window
{
    public event Action OnStartShooting = () => { };
    public event Action OnStopShooting = () => { };
    public event Action<Vector3> OnMove = (direction) => { };
    

    public void StickTouched(bool touch)
    {
        if (touch)
        {
            OnStopShooting();
        }
        else
        {
            OnStartShooting();
        }
    }

    public void StickMoved(Vector3 vector)
    {
        OnMove(vector);
    }
}
