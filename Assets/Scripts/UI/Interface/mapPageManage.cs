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
    private sortingSection sortingSection;

    [SerializeField]
    private UIplanetDescription planetDescription;

    public List<planetItem> initialItems = new List<planetItem>();

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

    private void HandleDescriptionRequest(int itemIndex)
    {
        planetDescription.gameObject.SetActive(true);
        planetItem planetItem = sortingSection.planetData.GetItemAt(itemIndex);
        if (planetItem.IsEmpty)
        {
            Debug.Log("get index");
            planetUI.ResetSelection();
            return;
        }
        planetSO item = planetItem.item;
        item.tesyingMethod(); // initail
        planetUI.UpdateDescription(itemIndex,item.planetImage, item.planetSymbol,
            item.Name, item.location, item.rank, item.uniqueness,
        item.Advantage, item.Disadvantage, item.resource);

        Debug.Log("resouce" + item.resource);
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
