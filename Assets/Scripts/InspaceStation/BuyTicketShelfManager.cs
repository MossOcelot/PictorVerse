using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyTicketShelfManager : MonoBehaviour
{
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

        playerTicket.playerStatus.player_accounts.setPocket(section, newValue);
        playerTicket.playerStatus.setEnergy(-data.stamina);
        ResetData();
    }

    public void ResetData()
    {
        LeaveStation = "";
        distance = 0;
        price = 0;
        stamina = 0;
    }

}
