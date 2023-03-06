using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortInventory : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage UIInventoryPlayer;
    [SerializeField]
    private InventorySO inventoryPlayer;
    public void SelectTest()
    {
        inventoryPlayer.SortItems();

        UIInventoryPlayer.ResetSelection();
        /* foreach (var item in inventoryPlayer.GetCurrentInventoryState())
        {
            UIInventoryPlayer.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);

        } */
    }
}
