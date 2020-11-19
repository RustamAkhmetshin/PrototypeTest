using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "PlayerConfiguration", menuName = "PlayerConfiguration" )]
public class PlayerConfiguration : ScriptableObject
{
    public float MaxHealth;
    public float MoveSpeed;
}
