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
    private planetSwitch planetSwitch;
    //[SerializeField]
    //private planetBoxSO planetData;

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
        PrepareUI();
        foreach (var item in planetSwitch.planetData.GetCurrentPlanetState())
        {
            planetUI.UpdateData(item.Key,
                item.Value.item.planetImage);
        }

        PreparePlanetData();

    }

    private void PreparePlanetData()
    {
        planetSwitch.planetData.Initialize();
        planetSwitch.planetData.OnplanetUpdated += UpdatePlanetUI;
        foreach (planetItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
                planetSwitch.planetData.AddItem(item);
        }
    }

    private void UpdatePlanetUI(Dictionary<int, planetItem> planetState)
    {
        //planetUI.ResetAllItems();
        
        foreach (var item in planetState)
        {
            
            planetUI.UpdateData(item.Key, item.Value.item.planetImage);
            Debug.Log("update444");
        }
    }

public void open_cityMap()
    {
        mapPage.cityMap.gameObject.SetActive(true);
    }
    public void open_galaxyMap()
    {
        if (isGalaxyOn != true)
        {
            mapPage.galaxyMap.gameObject.SetActive(!isCityON);
            isGalaxyOn = !isGalaxyOn;
        }
        mapPage.planet.gameObject.SetActive(false);
    }

    public void back_to_cityMap()
    {
        if (isGalaxyOn != false)
        {
            mapPage.galaxyMap.gameObject.SetActive(isCityON);
            isGalaxyOn = !isGalaxyOn;
        }
        //mapPage.planet.gameObject.SetActive(false);    
    }
    private void PrepareUI()
    {
        planetUI.InitializePlanetUI(planetSwitch.planetData.Size);
        //Debug.Log("des requeset");
        HandleDescriptionRequest(0);
        //Debug.Log("des requeset3");

    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        //Debug.Log("get index1");
        planetItem planetItem = planetSwitch.planetData.GetItemAt(itemIndex);
        //Debug.Log("get index");
        if (planetItem.IsEmpty)
        {
            planetUI.ResetSelection();
            return;
        }
        planetSO item = planetItem.item;
        Debug.Log("data");
        planetUI.UpdateDescription(itemIndex, item.planetSymbol,
            item.Name, item.location, item.rank, item.uniqueness,
        item.Advantage, item.Disadvantage, item.resource);
        Debug.Log(item.location);
    }


    private void Start()
    {
        mapPage.cityMap.SetActive(isCityON);

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
                Debug.Log("kuay");
                mapPage.cityMap.gameObject.SetActive(false);
            }
        }
        
    }
}
