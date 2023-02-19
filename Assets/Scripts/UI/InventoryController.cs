using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventorySO inventoryData;

    public List<InventoryItem> initialItems= new List<InventoryItem>();

    /*
    [SerializeField]
    private AudioClip dropClip;
    [SerializeField]
    private AudioSource audioSource;
    */
    public void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (var item in initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);

        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            return;
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if(itemAction != null)
        {
            inventoryUI.showItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName,()=> PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
        {
            return;
        }
        inventoryUI.CreateDraggedItem(inventoryItem.item.icon, inventoryItem.quantity);
    }


    private void HandleSwapItems(int itenIndex_1, int itenIndex_2)
    {
        inventoryData.SwapItems(itenIndex_1, itenIndex_2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false) 
            {
                inventoryUI.show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);

                }
            } else
            {
                inventoryUI.hide();
            }
        }
    }
}
