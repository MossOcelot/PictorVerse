using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniInventoryPage : MonoBehaviour
{
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

    private void Awake()
    {
        hide();
        mouseFollower.Toggle(false);
    }

    public void InitializeMiniInventoryUI(int inventorysize)
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
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;     
        }

    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
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

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
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

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
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

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    public void hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        ResetDraggtedItem();
    }
}
