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
    }
}
