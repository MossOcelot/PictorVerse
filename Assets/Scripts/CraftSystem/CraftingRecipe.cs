using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftingRecipe", menuName = "Crafting/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<InventoryItem> Materials;
    public List<InventoryItem> Results;

    public bool CanCraft(InventorySO CraftSlot)
    {
        foreach(InventoryItem itemAmount in Materials)
        {
            foreach (InventoryItem item in CraftSlot.GetCurrentInventoryState().Values)
            {
                if(item.item.item_id == itemAmount.item.item_id)
                {
                    if(item.quantity >= itemAmount.quantity) {
                        return true;
                    }
                }
            }
            return false;
        }
        return false;
    }

    public void Craft(InventorySO CraftSlot, InventorySO playerBag)
    {
        if(CanCraft(CraftSlot))
        {
            foreach(InventoryItem itemAmount in Materials)
            {
                Dictionary<int, InventoryItem> itemInCraft = CraftSlot.GetCurrentInventoryState();
                foreach(int index in itemInCraft.Keys)
                {
                    int amount = itemInCraft[index].quantity;
                    CraftSlot.RemoveItem(index, amount);
                }
            }

            foreach(InventoryItem itemAmount in Results) 
            {
                int quantity = playerBag.AddItem(itemAmount);
                if (quantity > 0 )
                {
                    Debug.Log("System send to Mail because Your bag is full");
                }
            }
        }
    }
}
