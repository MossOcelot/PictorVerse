using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private InventorySO slotData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemPickUp item = collision.GetComponent<itemPickUp>();
        if (item != null)
        {
            if(slotData.IsInventoryFull() != true)
            {
                int reminder = slotData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
            else
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
    }
}