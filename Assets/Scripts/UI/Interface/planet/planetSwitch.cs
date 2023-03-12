using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;
using UnityEngine.EventSystems;

public class planetSwitch : MonoBehaviour
{
    [SerializeField]
    public planetBoxSO planetData;
    bool isSwitchOn = false;
    bool switchOn = false;
    public void turnOn()
    {
        Debug.Log("Switch:" + isSwitchOn);
        isSwitchOn = true;
        //Debug.Log("is swicth on"+ isSwitchOn);
    }

    public bool passBool()
    {
        bool switchOn = isSwitchOn;
        isSwitchOn =false;
        return switchOn; 
    }
}
