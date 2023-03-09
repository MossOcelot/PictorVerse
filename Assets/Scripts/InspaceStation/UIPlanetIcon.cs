using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlanetIcon : MonoBehaviour
{
    public enum section { section1, section2, section3, section4, section5 }

    public GameObject ArriveMarker;
    public GameObject LeaveMarker;
    public GameObject TitleStatusActive;
    public GameObject TitleStatusBusy;

    public string PlanetName;
    public section PlanetInSection;

    public void SetArriveMarker(bool status)
    {
        ArriveMarker.SetActive(status);
    }

    public void SetLeaveMarker(bool status)
    {
        LeaveMarker.SetActive(status);
    }

    public void SetActiveStatus(bool status)
    {
        TitleStatusActive.SetActive(status);
        TitleStatusBusy.SetActive(!status);
    }
}
