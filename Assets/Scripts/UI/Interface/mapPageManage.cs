using inventory.Model;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


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
    private InventorySO resourceData;

    [SerializeField]
    private UIplanetDescription planetDescription;

    public List<planetItem> initialItems = new List<planetItem>();
    public List<InventoryItem> resource_initialItems = new List<InventoryItem>();

    bool isCityON = false;
    bool isGalaxyOn = false;
    int Size_r = 0;
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
        resourceData.OnInventoryUpdated += UpdateResourceUI;
        foreach (var item in resource_initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
           resourceData.AddItem(item);
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
        Debug.Log("i do");
        mapPage.open_window();

        isCityON = !isCityON;
        Debug.Log("bitch");
    }
    public void open_galaxyMap()
    {
        if (isGalaxyOn != true)
        {
            mapPage.galaxyMap.gameObject.SetActive(!isGalaxyOn);
            isGalaxyOn = !isGalaxyOn;
        }
        mapPage.planet.gameObject.SetActive(false);
        planetDescription.gameObject.SetActive(false);
    }

    public void back_to_cityMap()
    {
        if (isGalaxyOn != false)
        {
            mapPage.galaxyMap.gameObject.SetActive(!isGalaxyOn);
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
        //Debug.Log("resource" + sortingSection.resourceData.Size);
        resourceUI.InitializeResourceUI(10); // กำหนดให้ท่ากับ3

    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        planetDescription.gameObject.SetActive(true);
        planetItem planetItem = sortingSection.planetData.GetItemAt(itemIndex); //planetItem = ดาว1ดวงที่เลือก
        if (planetItem.IsEmpty)
        {
            planetUI.ResetSelection();
            return;
        }
        planetSO item = planetItem.item;
        planetUI.UpdateDescription(itemIndex, item.planetImage, item.planetSymbol,
            item.Name, item.location, item.rank, item.uniqueness,
        item.Advantage, item.Disadvantage);



        //sortingSection.index = itemIndex;
        //sortingSection.selectingResource();
        //Debug.Log("re2" + sortingSection.resourceData);
        resourceUI.gameObject.SetActive(true);
        resourceData = item.resourceSO;
        PrepareResourceUI();
        foreach (var r_item in item.resourceSO.GetCurrentInventoryState()) /// can use item.resourceSO
        {
            resourceUI.UpdateData(r_item.Key, r_item.Value.item, r_item.Value.item.icon);
        }
        InventoryItem resourceItem = item.resourceSO.GetItemAt(itemIndex);
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

        if (Input.GetKeyUp(KeyCode.M))
        {
            if (isCityON != true)
            {
                mapPage.open_window();

                isCityON = !isCityON;
            }
            else
            {
                mapPage.close_window();
                isCityON = !isCityON;
            }
        }

    }
}
