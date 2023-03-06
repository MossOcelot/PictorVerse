using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class mapPageManage : MonoBehaviour
{
    [SerializeField]
    private mapPage mapPage;

    [SerializeField]
    private UIPlanetPage planetUI;


    // [SerializeField]
    //private UIplanetInfoPage planetUI;

    bool isCityON = false;
    bool isGalaxyOn = false;

    public int planetSize = 1;
    public void close()
    {
        mapPage.close_window();
    }
    
    public void open_planetPage()
    {
        mapPage.planet.gameObject.SetActive(true);
        planetUI.InitializePlanetUI(planetSize);
        Debug.Log("initial");
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
