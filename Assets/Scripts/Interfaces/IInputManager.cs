using System;
using UnityEngine;

public interface IInputManager
{
    event Action OnStartShooting;
    event Action OnStopShooting;
    event Action<Vector3> OnMove;
    void InitInputManager();
}
