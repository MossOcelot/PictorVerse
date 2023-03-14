using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;
public class UIresourcePage : MonoBehaviour
{
    [SerializeField]
    private UIresourceItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIresourceItem> listOfUIItems = new List<UIresourceItem>();

    private void Awake()
    {
        Hide();
    }
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIresourceItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            int index = listOfUIItems.IndexOf(uiItem);
        }
    }

    public void UpdateData(int itemIndex, Item item, Sprite itemImage)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemIndex, item, itemImage);
        }
    }

    public void DeletePlanetUI()
    {
        int range = contentPanel.childCount;
        for (int i = 0; i < range; i++)
        {
            Destroy(contentPanel.GetChild(i).gameObject);
        }
    }

    public void Show()
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
        foreach (UIresourceItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
