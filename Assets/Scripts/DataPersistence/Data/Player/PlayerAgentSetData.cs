using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAgentSetData
{
    public Item cap;
    public Item bag;
    public Item chestplate;
    public Item leggings;
    public Item boots;

    public List<ItemParameter> itemCurrentState_cap;
    public List<ItemParameter> itemCurrentState_bag;
    public List<ItemParameter> itemCurrentState_chestplate;
    public List<ItemParameter> itemCurrentState_leggings;
    public List<ItemParameter> itemCurrentState_boots;

    public PlayerAgentSetData(AgentSet agentSet)
    {
        cap = agentSet.GetCap();
        bag = agentSet.GetBag();
        chestplate= agentSet.GetChestplate();
        leggings= agentSet.GetLeggings();
        boots= agentSet.GetBoots();
        itemCurrentState_cap = agentSet.GetItemCurrentState_cap();
        itemCurrentState_bag = agentSet.GetItemCurrentState_bag();
        itemCurrentState_chestplate = agentSet.GetItemCurrentState_chestplate();
        itemCurrentState_leggings = agentSet.GetItemCurrentState_leggings();
        itemCurrentState_boots = agentSet.GetItemCurrentState_boots();  
    }
}
