using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlanetIcon : MonoBehaviour
{
    [SerializeField]
    private BuyTicketShelfManager buyTicketShelfManager;
    public enum section { section1, section2, section3, section4, section5 }

    [SerializeField]
    private TicketController playerTicket;
    public GameObject ArriveMarker;
    public GameObject LeaveMarker;
    public GameObject TitleStatusActive;
    public GameObject TitleStatusBusy;

    public string PlanetName;
    public section PlanetInSection;

    private bool IsArriveStation = false;
    [SerializeField]
    private bool IsActive = false;
    public bool IsClick = false;
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
        this.IsActive = status;
        TitleStatusBusy.SetActive(!status);
    }
    public void SetIsArriveStation(bool status)
    {
        this.IsArriveStation = status;
    }

    private void Start()
    {
        playerTicket = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<TicketController>();
        buyTicketShelfManager = GameObject.FindGameObjectWithTag("BuyTicketShelf").gameObject.GetComponent<BuyTicketShelfManager>();
    }

    private void Update()
    {
        string oldLeaveStation = buyTicketShelfManager.GetLeaveStation();
        if(oldLeaveStation != PlanetName)
        {
            LeaveMarker.SetActive(false);
        }


        if (IsArriveStation)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        } else
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void UseLeaveStation()
    {
        if (IsActive && !IsClick)
        {
            buyTicketShelfManager.SetLeaveStation(PlanetName);
            IsClick = true;
            LeaveMarker.SetActive(true);
        } else
        {
            IsClick = false;
            buyTicketShelfManager.SetLeaveStation("");
            LeaveMarker.SetActive(false);
        }
    }
}
