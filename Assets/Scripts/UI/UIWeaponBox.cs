using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponBox : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private MouseFollower mouseFollower;

    public event Action<int> OnItemActionRequested;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    [SerializeField]
    private ItemActionPanel actionPanel;

    private void Awake()
    {
        hide();
        mouseFollower.Toggle(false);
    }

    public void InitializeWeaponBoxUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
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

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
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

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    public void showItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
    }

    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButon(actionName, performAction);
    }

    public void hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
    }
}
