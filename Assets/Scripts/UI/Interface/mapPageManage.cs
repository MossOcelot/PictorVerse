using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class mapPageManage : MonoBehaviour
{
    [SerializeField]
    private mapPage mapPage;

    bool isCityON = false;
    bool isGalaxyOn = false;
    public void close()
    {
        mapPage.close_window();
    }
    
    public void open_planetPage()
    {
        mapPage.planet.gameObject.SetActive(true);
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGalaxyOn)
        {
            mapPage.cityMap.SetActive(false);
        }
        else {  mapPage.cityMap.SetActive(true);}

        if(Input.GetKeyUp(KeyCode.M))
        {
            
        }
    }
}
