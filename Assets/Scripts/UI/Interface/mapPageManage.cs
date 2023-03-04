using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void open_galaxyMap()
    {
        isGalaxyOn = true;
        mapPage.galaxyMap.gameObject.SetActive(true);
    }

    public void back_to_cityMap()
    {
        isGalaxyOn = false;
        mapPage.galaxyMap.gameObject.SetActive(false);
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
