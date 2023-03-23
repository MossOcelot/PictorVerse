using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    //[SerializeField]
    //private RectTransform itemDescription;

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
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0;i < inventorysize;i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnEnterMouseBtn += HandleShowItemDescription;
        }
    }

    public void UpdateData(int itemIndex, Item item, Sprite itemImage, int itemQuantity)
    {
        if(listOfUIItems.Count > itemIndex)
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

    private void HandleShowItemDescription(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
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
        if(index == -1)
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
        int index= listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1)
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
        descriptionActionPanel.Toggle(false);
        actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
    }

    public void AddDescription(string item_name, int quantity, float item_price, string description)
    {
        descriptionActionPanel.AddDescription(item_name, quantity, item_price, description);
    }

    public void AddActionInDescription(int n,string actionName, Action action)
    {
        Debug.Log($"N: {n} actionName: {actionName}");
        descriptionActionPanel.AddAction(n,actionName, action);
    }

    public void showItemDescriptionAction(int itemIndex)
    {
        actionPanel.Toggle(false);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
        descriptionActionPanel.Toggle(true);
        descriptionActionPanel.transform.position = listOfUIItems[itemIndex].transform.position + new Vector3(105.7f, -187f, 0);
    }

    public void hideItem(int itemIndex)
    {
        actionPanel.Toggle(false);
        DeselectAllItems();
        descriptionActionPanel.Toggle(false);
    }
    
    private void DeselectAllItems()
    {
        foreach(UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(false);
    }

    public void hide()
    {
        resetItem();
        gameObject.SetActive(false);
    }

    public void resetItem()
    {
        ResetDraggtedItem();
        actionPanel.Toggle(false);
        descriptionActionPanel.Toggle(false);
    }

    
}
