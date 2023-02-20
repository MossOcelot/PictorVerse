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
    private UIWeaponBox WeaponBoxUI;
    [SerializeField]
    private UIMiniInventoryPage miniInventoryUI;

    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private InventorySO WeaponBoxData;
    [SerializeField]
    private InventorySO miniInventoryData;

    public List<InventoryItem> initialItems= new List<InventoryItem>();
    public List<InventoryItem> WeaponItems= new List<InventoryItem>();
    public List<InventoryItem> miniInitialItem= new List<InventoryItem>();
    /*
    [SerializeField]
    private AudioClip dropClip;
    [SerializeField]
    private AudioSource audioSource;
    */
    public void Start()
    {
        PrepareInventoryUI();
        PrepareInventoryData();

        PrepareWeaponBoxUI();
        PrepareWeaponBoxData();

        PrepareMiniInventoryUI();
        PrepareMiniInventoryData();
    }

    //
    public void UseItem(EatableItem EatableItem, int quantity, List<ItemParameter> itemState)
    {
        miniInventoryData.AddItem(EatableItem, quantity, itemState);
    }

    public void NotUseItem(EatableItem EatableItem, int quantity, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(EatableItem, quantity, itemState);
    }
    //

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

    private void PrepareWeaponBoxData()
    {
        WeaponBoxData.Initialize();
        WeaponBoxData.OnInventoryUpdated += UpdateWeaponBoxUI;
        foreach (var item in WeaponItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            WeaponBoxData.AddItem(item);
        }
    }

    private void PrepareMiniInventoryData()
    {
        miniInventoryData.Initialize();
        miniInventoryData.OnInventoryUpdated += UpdateminiInventoryUI;
        foreach (var item in miniInitialItem)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            miniInventoryData.AddItem(item);
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

    private void UpdateWeaponBoxUI(Dictionary<int, InventoryItem> inventoryState)
    {
        WeaponBoxUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            WeaponBoxUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);

        }
    }

    public void UpdateminiInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        miniInventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            miniInventoryUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);
        }
    }

    private void PrepareInventoryUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void PrepareWeaponBoxUI()
    {
        WeaponBoxUI.InitializeWeaponBoxUI(WeaponBoxData.Size);
        this.WeaponBoxUI.OnItemActionRequested += HandleItemActionRequestWeaponBox;
    }

    private void PrepareMiniInventoryUI()
    {
        miniInventoryUI.InitializeMiniInventoryUI(inventoryData.Size);
        this.miniInventoryUI.OnSwapItems += HandleMiniSwapItems;
        this.miniInventoryUI.OnStartDragging += HandleMiniDragging;
        this.miniInventoryUI.OnItemActionRequested += HandleMiniItemActionRequest;
    }

    private void HandleMiniDragging(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventoryData.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
        {
            return;
        }
        miniInventoryUI.CreateDraggedItem(miniInventoryItem.item.icon, miniInventoryItem.quantity);
    }

    private void HandleMiniSwapItems(int itemIndex1, int itemIndex2)
    {
        miniInventoryData.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleMiniItemActionRequest(int itemIndex)
    {
        InventoryItem MiniItem = miniInventoryData.GetItemAt(itemIndex);
        if (MiniItem.IsEmpty)
        {
            return;
        }

        IItemAction itemAction = MiniItem.item as IItemAction;
        if (itemAction != null)
        {
            miniInventoryUI.showItemAction(itemIndex);
            miniInventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IUSEAction itemUSEAction = MiniItem.item as IUSEAction;
        if (itemUSEAction != null)
        {
            miniInventoryUI.AddAction("Notuse", () => NotUseAction(itemIndex));
        }

        IDestroyableItem destroyableItem = MiniItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventoryUI.AddAction("Drop", () => DropItem(itemIndex, MiniItem.quantity));
        }
    }

    private void HandleItemActionRequestWeaponBox(int itemIndex)
    {
        InventoryItem WeaponItem = WeaponBoxData.GetItemAt(itemIndex);
        if (WeaponItem.IsEmpty)
        {
            return;
        }

        IItemAction itemAction = WeaponItem.item as IItemAction;
        if (itemAction != null)
        {
            WeaponBoxUI.showItemAction(itemIndex);
            WeaponBoxUI.AddAction(itemAction.NoActionName, () => NoperformAction(itemIndex));
        }
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

        IUSEAction itemUseAction = inventoryItem.item as IUSEAction;
        if (itemUseAction != null)
        {
            inventoryUI.AddAction("use", () => UseAction(itemIndex));
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

    public void NoperformAction(int itemIndex)
    {
        InventoryItem inventoryItem = WeaponBoxData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            WeaponBoxData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.NoperformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (WeaponBoxData.GetItemAt(itemIndex).IsEmpty)
                WeaponBoxUI.ResetSelection();
        }
    }

    public void UseAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, inventoryItem.quantity);
        }

        IUSEAction itemAction = inventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            itemAction.UseAction(gameObject, inventoryItem.quantity, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
    }

    public void NotUseAction(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventoryData.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = miniInventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventoryData.RemoveItem(itemIndex, miniInventoryItem.quantity);
        }

        IUSEAction itemAction = miniInventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            itemAction.NotUseAction(gameObject, miniInventoryItem.quantity, miniInventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (miniInventoryData.GetItemAt(itemIndex).IsEmpty)
                miniInventoryUI.ResetSelection();
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
                WeaponBoxUI.show();
                miniInventoryUI.show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);
                    
                }

                foreach(var item in WeaponBoxData.GetCurrentInventoryState())
                {
                    WeaponBoxUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);
                }

                foreach (var item in miniInventoryData.GetCurrentInventoryState())
                {
                    miniInventoryUI.UpdateData(item.Key, item.Value.item.icon, item.Value.quantity);
                }
            } else
            {
                inventoryUI.hide();
                WeaponBoxUI.hide();
                miniInventoryUI.hide();
            }
        }
    }
}
