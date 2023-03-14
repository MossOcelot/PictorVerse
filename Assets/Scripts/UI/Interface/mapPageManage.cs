using inventory.Model;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI;

public class mapPageManage : MonoBehaviour
{
    [SerializeField]
    private mapPage mapPage;

    [SerializeField]
    private UIPlanetPage planetUI;

    [SerializeField]
    private UIresourcePage resourceUI;

    [SerializeField]
    private sortingSection sortingSection;

    [SerializeField]
    private UIplanetDescription planetDescription;

    public List<planetItem> initialItems = new List<planetItem>();
    public List<InventoryItem> resource_initialItems = new List<InventoryItem>();

    bool isCityON = false;
    bool isGalaxyOn = false;

    //public int planetSize = 2;
    public void close()
    {
        mapPage.close_window();
    }

    public void open_planetPage()
    {
        mapPage.planet.gameObject.SetActive(true);
        
        planetUI.Show();
        sortingSection.sortSection();
        PrepareUI();
        foreach (var item in sortingSection.planetData.GetCurrentPlanetState())
        {
            planetUI.UpdateData(item.Key,
                item.Value.item.planetImage);
        }
        PreparePlanetData();

    }

    private void PreparePlanetData()
    {
        sortingSection.planetData.OnplanetUpdated += UpdatePlanetUI;
        foreach (planetItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
                sortingSection.planetData.AddItem(item);
                
        }
    }

    private void PrepareResourceData()
    {
        sortingSection.resourceData.OnInventoryUpdated += UpdateResourceUI;
        foreach (var item in resource_initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            sortingSection.resourceData.AddItem(item);
        }
    }

    private void UpdateResourceUI(Dictionary<int, InventoryItem> resourceState)
    {
        resourceUI.ResetAllItems();
        foreach (var item in resourceState)
        {
            resourceUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon);

        }
    }

    private void UpdatePlanetUI(Dictionary<int, planetItem> planetState)
    {
        foreach (var item in planetState)
        {    
            planetUI.UpdateData(item.Key, item.Value.item.planetImage);
        }
    }

public void open_cityMap()
    {
        mapPage.gameObject.SetActive(true);
    }
    public void open_galaxyMap()
    {
        if (isGalaxyOn != true)
        {
            mapPage.galaxyMap.gameObject.SetActive(!isCityON);
            isGalaxyOn = !isGalaxyOn;
        }
        mapPage.planet.gameObject.SetActive(false);
        planetDescription.gameObject.SetActive(false);
        //planetUI.DeletePlanetUI();
    }

    public void back_to_cityMap()
    {
        if (isGalaxyOn != false)
        {
            mapPage.galaxyMap.gameObject.SetActive(isCityON);
            isGalaxyOn = !isGalaxyOn;
        }
        //planetUI.DeletePlanetUI();   
    }
    private void PrepareUI()
    {
        planetUI.InitializePlanetUI(sortingSection.planetData.Size);
        this.planetUI.OnDescriptionRequested += HandleDescriptionRequest;

    }

    private void PrepareResourceUI()
    {
        Debug.Log("resource" + sortingSection.resourceData.Size);
        resourceUI.InitializeResourceUI(sortingSection.resourceData.Size);

    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        Debug.Log("item index" + itemIndex);
        Debug.Log("re1" + sortingSection.resourceData);
        planetDescription.gameObject.SetActive(true);
        planetItem planetItem = sortingSection.planetData.GetItemAt(itemIndex);
        if (planetItem.IsEmpty)
        {
            Debug.Log("get index");
            planetUI.ResetSelection();
            return;
        }
        planetSO item = planetItem.item;
        planetUI.UpdateDescription(itemIndex,item.planetImage, item.planetSymbol,
            item.Name, item.location, item.rank, item.uniqueness,
        item.Advantage, item.Disadvantage);

        Debug.Log("re index1" + sortingSection.index);
        sortingSection.index = itemIndex;
        Debug.Log("re index2" + sortingSection.index);
        sortingSection.selectingResource();
        Debug.Log("re2" + sortingSection.resourceData);
        resourceUI.gameObject.SetActive(true);
        PrepareResourceUI();
        foreach (var r_item in sortingSection.resourceData.GetCurrentInventoryState())
        {
            resourceUI.UpdateData(r_item.Key, r_item.Value.item, r_item.Value.item.icon);
        }
        InventoryItem resourceItem = sortingSection.resourceData.GetItemAt(itemIndex);
        // inventoryUI
        if (resourceItem.IsEmpty)
        {
            resourceUI.ResetSelection();
            return;
        }
        PrepareResourceData();
        //inventory.Model.Item resource_item = resourceItem.item;
        //resourceUI.UpdateResorce(itemIndex, resource_item.icon);
    }


    private void Start()
    {
        mapPage.cityMap.SetActive(true);

    }

    // Update is called once per frame
    public void Update()
    {

        if(Input.GetKeyUp(KeyCode.Q))
        {
            if (isCityON!=true)
            {
                mapPage.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("kuay");
                mapPage.gameObject.SetActive(false);
            }
        }
        
    }
}
