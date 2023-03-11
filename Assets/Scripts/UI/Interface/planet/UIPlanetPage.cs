using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class UIPlanetPage : MonoBehaviour
{
    [SerializeField]
    private UIplanetItem planetPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIplanetDescription planetDescription;

    List<UIplanetItem> listOfUIItems = new List<UIplanetItem>();

    public event Action<int> OnDescriptionRequested;

    public void UpdateData(int itemIndex,
        Sprite itemImage)
    {
        Debug.Log("list"+ listOfUIItems.Count);
        Debug.Log("itemIndex"+itemIndex);
        Debug.Log("updatedata out");
        if (listOfUIItems.Count > itemIndex)
        {
            Debug.Log("updatedata in");
            listOfUIItems[itemIndex].SetData(itemImage);
        }
    }

    private void Awake()
    {
        Hide();
        planetDescription.ResetDescription();
    }

    public void InitializePlanetUI(int inventorysize)
    {
        if (listOfUIItems.Count == 0)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIplanetItem uiItem = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                HandleItemSelection(uiItem);
                int index = listOfUIItems.IndexOf(uiItem);
                OnDescriptionRequested?.Invoke(index);
            }
        }
    }

    private void HandleItemSelection(UIplanetItem planetItemUI)
    {
        int index = listOfUIItems.IndexOf(planetItemUI);
        if (index == -1)
            return;
        Debug.Log("dkowwdo");
        //OnDescriptionRequested?.Invoke(index);
        Debug.Log(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ResetSelection()
    {
        planetDescription.ResetDescription();
        //listOfUIItems[0].SetData(planetSprite);
        //planetDescription.SetDescription(planetSymbol, planetName,
        //planetLocation, rank, unique, advantage, disadvantage, resource);
        //listOfUIItems[0].Select();
        DeselectAllItems();
    }


    private void DeselectAllItems()
    {
        foreach (UIplanetItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void UpdateDescription(int itemIndex, Sprite planetSymbol, string name, 
        string location, string rank, string uniqueness, string advantage, string disadvantage, Sprite resource)
    {
        planetDescription.SetDescription(planetSymbol, name, location, rank, uniqueness, advantage, disadvantage, resource);
        //DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }

    internal void ResetAllItems()
    {
        foreach(var item in listOfUIItems)
            {
            item.ResetData();
            item.Deselect();
        }
    }
}
