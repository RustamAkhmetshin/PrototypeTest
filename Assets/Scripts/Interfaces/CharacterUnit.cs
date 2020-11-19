
using System;
using System.Data.Common;
using UnityEngine;

public abstract class CharacterUnit
{
    [Serializable]
    protected class UnitConfigurationData
    {
        public float MaxHealth;
    }

    [Serializable]
    protected class UnitStateData
    {
        public float Health;
    }
    
    public enum UnitState
    {
        IsMoving,
        IsShooting,
        IsNotAlive,
        Idle,
    }

    protected UnitConfigurationData Configuration;
    protected UnitStateData StateData;
    
    public float CurrentHealth
    {
        get { return StateData.Health; }
        set { StateData.Health = value; }
    }
    
    protected UnitState CurrentUnitState;

    public CharacterUnit(float maxHealth)
    {
        Configuration = new UnitConfigurationData();
        StateData = new UnitStateData();
        this.Configuration.MaxHealth = maxHealth;
        this.CurrentHealth = maxHealth;
        CurrentUnitState = UnitState.Idle;
    }
    
    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate(float fixedDeltaTime);
    public abstract void Hit(Collider collider);
}



