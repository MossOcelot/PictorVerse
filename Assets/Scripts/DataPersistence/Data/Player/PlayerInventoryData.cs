using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryData
{
    public List<InventoryItem> initialItems;
    public List<InventoryItem> WeaponItems;
    public List<InventoryItem> miniInitialItem;

    public PlayerInventoryData(InventoryController inventoryController) 
    { 
        initialItems = inventoryController.initialItems;
        WeaponItems = inventoryController.WeaponItems;  
        miniInitialItem = inventoryController.miniInitialItem;  
    }
}
