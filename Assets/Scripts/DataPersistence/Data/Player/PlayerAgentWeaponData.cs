using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgentWeaponData
{
    public Item weapon1;
    public List<ItemParameter> itemCurrentState;
    public Item weapon2;
    public List<ItemParameter> itemCurrentState2;

    public PlayerAgentWeaponData(AgentWeapon agentWeapon)
    {
        weapon1 = agentWeapon.GetWeaponItem();
        weapon2 = agentWeapon.GetWeaponItem2();
        itemCurrentState = agentWeapon.GetItemCurrentState();
        itemCurrentState2 = agentWeapon.GetItemCurrentState2();
    }
}
