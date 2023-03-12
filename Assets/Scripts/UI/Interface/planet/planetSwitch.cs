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
        isSwitchOn = true;
    }

    public bool passBool()
    {
        bool switchOn = isSwitchOn;
        isSwitchOn =false;
        return switchOn; 
    }
}
