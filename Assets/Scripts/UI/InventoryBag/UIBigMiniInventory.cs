using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBigMiniInventory : MonoBehaviour
{
    [SerializeField]
    private InventorySO miniInventory;

    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int currentlyDraggedItemIndex = -1; // -1 หมายความว่าอยู่นอกขอบเขต List<UIInventoryItem>

    public event Action<int> OnDescriptionRequested,
        OnItemActionRequested,
        OnStartDragging;
    public event Action<int, int> OnSwapItems;

    [SerializeField]
    private ItemActionPanel actionPanel;
    [SerializeField]
    private ItemDescribtionAction descriptionActionPanel;

    private void Awake()
    {
        hide();
        mouseFollower.Toggle(false);
    }

    private void Start()
    {
        InitializeBigMiniInventoryUI(miniInventory.Size);
        foreach (var item in miniInventory.GetCurrentInventoryState())
        {
            UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
        }

        OnSwapItems += HandleMiniSwapItems;
        OnStartDragging += HandleMiniDragging;
        OnDescriptionRequested += HandleMiniItemDescriptionRequest;
    }

    public void InitializeBigMiniInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnEnterMouseBtn += HandleItemSelection;
        }

    }

    public void UpdateData(int itemIndex, Item item, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemIndex, item, itemImage, itemQuantity);
        }
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggtedItem();
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItemUI);
    }

    private void ResetDraggtedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(int itemIndex, Item item, Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(itemIndex, item, sprite, quantity);
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }

    public void show()
    {
        gameObject.SetActive(true);

        ResetSelection();
    }

    public void ResetSelection()
    {
        DeselectAllItems();
    }

    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButon(actionName, performAction);
    }
    public void showItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
    }

    public void AddDescription(string item_name, int quantity, float item_price, string description)
    {
        descriptionActionPanel.AddDescription(item_name, quantity, item_price, description);
    }

    public void AddActionInDescription(int n, string actionName, Action action)
    {
        descriptionActionPanel.AddAction(n, actionName, action);
    }

    public void showItemDescriptionAction(int itemIndex)
    {
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(true);
        descriptionActionPanel.transform.position = listOfUIItems[itemIndex].transform.position + new Vector3(105.7f, 187f, 0);
    }

    public void hideItem(int itemIndex)
    {
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(false);
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(false);
    }

    public void hide()
    {
        resetItem();
    }

    public void resetItem()
    {
        ResetDraggtedItem();
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(false);
    }

    private void HandleMiniDragging(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventory.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
        {
            return;
        }
        CreateDraggedItem(itemIndex, miniInventoryItem.item, miniInventoryItem.item.icon, miniInventoryItem.quantity);
    }

    private void HandleMiniSwapItems(int itemIndex1, int itemIndex2)
    {
        miniInventory.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleMiniItemDescriptionRequest(int itemIndex)
    {
        InventoryItem MiniItem = miniInventory.GetItemAt(itemIndex);
        if (MiniItem.IsEmpty)
        {
            hideItem(itemIndex);
            return;
        }

        //ddata
        string item_name = MiniItem.item.item_name;
        int item_quantity = MiniItem.quantity;
        float item_price = MiniItem.price;
        string item_description = MiniItem.item.description;

        showItemDescriptionAction(itemIndex);
        AddDescription(item_name, item_quantity, item_price, item_description);

        IItemAction itemAction = MiniItem.item as IItemAction;
        if (itemAction != null)
        {
            AddActionInDescription(0, itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IUSEAction itemUSEAction = MiniItem.item as IUSEAction;
        if (itemUSEAction != null)
        {
            AddActionInDescription(1, "Notuse", () => NotUseAction(itemIndex));
        }

        IDestroyableItem destroyableItem = MiniItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            AddActionInDescription(2, "Drop", () => DropItem(itemIndex, MiniItem.quantity));
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
       miniInventory.RemoveItem(itemIndex, quantity);
       ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }

    public void PerformAction(int itemIndex)
    {
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        if (playerStatus.getHP() >= playerStatus.getMaxHP() && playerStatus.getEnergy() >= playerStatus.getMaxEnergy()) return;

        InventoryItem inventoryItem = miniInventory.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventory.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (miniInventory.GetItemAt(itemIndex).IsEmpty)
                ResetSelection();
        }
    }

    public void NotUseAction(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventory.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = miniInventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventory.RemoveItem(itemIndex, miniInventoryItem.quantity);
        }

        IUSEAction itemAction = miniInventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player").gameObject;
            itemAction.NotUseAction(PlayerObject, miniInventoryItem.quantity, miniInventoryItem.itemState);


            //audioSource.PlayOneShot(itemAction.actionSFX
            if (miniInventory.GetItemAt(itemIndex).IsEmpty)
                ResetSelection();

        }
    }
}
