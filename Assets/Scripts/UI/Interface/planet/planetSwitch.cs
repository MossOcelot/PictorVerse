using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;
using UnityEngine.EventSystems;

public class planetSwitch : MonoBehaviour
{
    [SerializeField]
    public planetBoxSO planetData;

    public InventorySO resourceData = null;

    [SerializeField]
    public InventorySO resourceData1;

    [SerializeField]
    public InventorySO resourceData2;


    bool isSwitchOn = false;
    public void turnOn()
    {
        isSwitchOn = true;
    }

    public bool passBool()
    {
        bool switchOn = isSwitchOn;
        
        return switchOn; 
    }

    public void selectSection(int index)
    {
        if(index == 0 )
        {
            resourceData = resourceData1;
        }
        else if (index == 1 ) 
        {
            resourceData = resourceData2;
        }
        isSwitchOn = false;
    }
}
