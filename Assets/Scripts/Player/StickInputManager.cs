using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickInputManager : MonoBehaviour, IInputManager
{
    public GameObject manager;
    
    public event Action OnStartShooting = () => { };
    public event Action OnStopShooting = () => { };
    public event Action<Vector3> OnMove = (v) => { };
    
    
    public void InitInputManager()
    {
        var joystick = Root.UIManager.InitializeWindow<JoystickUIComponent>(manager,
            Root.UIManager.GetMainCanvas().transform);

        joystick.OnStartShooting += OnStartShooting;
        joystick.OnStopShooting += OnStopShooting;
        joystick.OnMove += OnMove;
    }
}
