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
        Debug.Log("Q");
        mapPage.planet.gameObject.SetActive(true);
        planetUI.Show();
        sortingSection.sortSection();
        Debug.Log("k");
        PrepareUI();
        foreach (var item in sortingSection.planetData.GetCurrentPlanetState())
        {
            Debug.Log("Key: " + item.Key);
            planetUI.UpdateData(item.Key,
                item.Value.item.planetImage);

        }
        PreparePlanetData();
    }

    private void PreparePlanetData()
    {
       // sortingSection.planetData.Initialize();
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
        //planetUI.ResetAllItems();
        foreach (var item in planetState)
        {    
            planetUI.UpdateData(item.Key, item.Value.item.planetImage);
            //Debug.Log("update444");
        }
    }

    public void open_cityMap()
    {
        mapPage.cityMap.gameObject.SetActive(true);
    }
    public void open_galaxyMap()
    {
        Debug.Log("D");
        if (isGalaxyOn != true)
        {
            mapPage.galaxyMap.gameObject.SetActive(!isCityON);
            isGalaxyOn = !isGalaxyOn;
        }
        Debug.Log("A");
        mapPage.planet.gameObject.SetActive(false);
        Debug.Log("V");
        planetDescription.gameObject.SetActive(false);
        Debug.Log("B");
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
        //Debug.Log("get index1");
        planetItem planetItem = sortingSection.planetData.GetItemAt(itemIndex);
        //Debug.Log("get index");
        if (planetItem.IsEmpty)
        {
            Debug.Log("get index");
            planetUI.ResetSelection();
            return;
        }
        planetSO item = planetItem.item;
        planetUI.UpdateDescription(itemIndex,item.planetImage, item.planetSymbol,
            item.Name, item.location, item.rank, item.uniqueness,
        item.Advantage, item.Disadvantage, item.resource);
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
                mapPage.cityMap.gameObject.SetActive(true);
            }
            else
            {
                mapPage.cityMap.gameObject.SetActive(false);
            }
        }
        
    }
}
