using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private AudioSource CollectSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickup item = collision.GetComponent<ItemPickup>();
        if(item != null )
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);

            if(reminder == 0)
            {
                CollectSFX.Play();
                item.DestroyItem();
            } else
            {
                item.Quantity = reminder;
            }
        }
    }
}
