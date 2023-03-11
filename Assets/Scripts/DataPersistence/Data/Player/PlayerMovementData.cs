using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovementData
{

    public float defaultMoveSpeed;

    public float walk_distance;
    public bool iswalk;
    public bool isDashButtonDown;

    public int energy_for_walk;
    public float strength;
    public float weight_player;

    public PlayerMovementData(PlayerMovement player_movement)
    {
        defaultMoveSpeed = player_movement.getDefaultMoveSpeed();
        walk_distance = player_movement.walk_distance;
        iswalk = player_movement.iswalk;
        isDashButtonDown = player_movement.isDashButtonDown;
        energy_for_walk = player_movement.energy_for_walk;
        strength = player_movement.strength;
        weight_player = player_movement.weight_player;
    }
}
