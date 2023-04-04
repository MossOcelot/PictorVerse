using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyTicketShelfManager : MonoBehaviour
{
    public GameObject NPC;
    public InSpaceDB inSpaceDB;
    public string section;
    public string planet_section;
    public TicketController playerTicket;
    public GameObject topMap;
    public Button BuyBtn;
    
    [SerializeField]
    private string LeaveStation = "";
    public float distance;
    public float price;
    public int stamina;

    public void SetLeaveStation(string newLeaveStation)
    {
        this.LeaveStation = newLeaveStation;
    }

    public string GetLeaveStation()
    {
        return LeaveStation;
    }

    // Start is called before the first frame update

    private void Awake()
    {
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        planet_section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().scenename;
        playerTicket = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<TicketController>();
        inSpaceDB = GameObject.FindGameObjectWithTag("TicketCompany").gameObject.GetComponent<InSpaceDB>();
    }
    void Start()
    {

        int PlanetCount = topMap.transform.childCount;
        for(int i = 0; i < PlanetCount; i++) 
        {
            UIPlanetIcon planetIcon = topMap.transform.GetChild(i).gameObject.GetComponent<UIPlanetIcon>();

            if(planetIcon.PlanetName == planet_section && planetIcon.PlanetInSection.ToString() == section)
            {
                planetIcon.SetArriveMarker(true);
                planetIcon.SetLeaveMarker(false);
                planetIcon.SetIsArriveStation(true);
                break;
            }
        }
    }

    private void Update()
    {
        if(LeaveStation != "" && playerTicket.GetTicket().LeaveStation == "")
        {
            float myMoney = playerTicket.playerStatus.player_accounts.getPocket()[section];
            int myStamina = playerTicket.playerStatus.getEnergy();
            ArriveToLeaveData data = inSpaceDB.GetArriveToLeaveData(planet_section, LeaveStation);
            distance = data.distance;
            price = data.price;
            stamina = data.stamina;
            Debug.Log("player: " + myMoney + " Price: " + price);
            if(myMoney >= data.price && myStamina >= data.stamina)
            {

                BuyBtn.interactable = true;
            } else
            {
                BuyBtn.interactable = false;
            }
        } else
        {
            BuyBtn.interactable = false;
            distance = 0;
            price = 0;
            stamina = 0;
        }

    }

    public void BuyTicket()
    {
        float myMoney = playerTicket.playerStatus.player_accounts.getPocket()[section];
        ArriveToLeaveData data = inSpaceDB.GetArriveToLeaveData(planet_section, LeaveStation);

        string playerName = playerTicket.playerStatus.getPlayerName();
        TicketInspace newTicket = new TicketInspace();
        newTicket.SetTicket(data.ArriveStation, data.LeaveStation, playerName, data.price, data.distance, data.stamina);
        playerTicket.AddTicket(newTicket);

        // update money and price
        float newValue = myMoney - data.price;

        Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        int[] dateTime = date.getDateTime();
        AccountsDetail newAccountDetail = new AccountsDetail() { date = dateTime, accounts_name = $"buy Ticket {planet_section} to {LeaveStation}", account_type = "buy", income = 0, expense = data.price };
        playerTicket.playerStatus.addAccountsDetails(newAccountDetail);

        TicketCompanyStatus company = GameObject.FindGameObjectWithTag("TicketCompany").gameObject.GetComponent<TicketCompanyStatus>();
        AccountsDetail newAcompanyccountDetail = new AccountsDetail() { date = dateTime, accounts_name = $"sell Ticket {planet_section} to {LeaveStation}", account_type = "sell", income = data.price, expense = 0 };
        company.addAccountsDetails(newAcompanyccountDetail);
        float newCompanyValue = company.companyData.pocketCompany.getPocket()[section] + data.price;
        company.companyData.pocketCompany.setPocket(section, newCompanyValue);

        playerTicket.playerStatus.player_accounts.setPocket(section, newValue);
        playerTicket.playerStatus.setEnergy(-data.stamina);
        ResetData();
        Close();
    }

    public void Close()
    {
        NPC.gameObject.GetComponent<NPCController>().playerIsClose = true;
        gameObject.SetActive(false);
    }

    public void ResetData()
    {
        LeaveStation = "";
        distance = 0;
        price = 0;
        stamina = 0;
    }

}
