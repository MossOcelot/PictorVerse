using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTicketShelfManager : MonoBehaviour
{
    public string section;
    public string planet_section;

    public GameObject topMap;
    // Start is called before the first frame update

    private void Awake()
    {
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        planet_section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().scenename;
    }
    void Start()
    {

        int PlanetCount = topMap.transform.childCount;
        for(int i = 0; i < PlanetCount; i++) 
        {
            Debug.Log("SSS: " + i);
            UIPlanetIcon planetIcon = topMap.transform.GetChild(i).gameObject.GetComponent<UIPlanetIcon>();

            if(planetIcon.PlanetName == planet_section && planetIcon.PlanetInSection.ToString() == section)
            {
                planetIcon.SetArriveMarker(true);
                planetIcon.SetLeaveMarker(false);
                break;
            }
        }
    }
}
