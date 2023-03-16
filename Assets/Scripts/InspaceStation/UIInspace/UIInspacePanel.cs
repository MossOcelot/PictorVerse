using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIInspacePanel : MonoBehaviour
{
    [SerializeField]
    private BuyTicketShelfManager buyTicketShelfManager;

    public TextMeshProUGUI ArriveStationText;
    public TextMeshProUGUI LeaveStationText;
    public TextMeshProUGUI TicketPriceText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI distanceText;

    // Update is called once per frame
    void Update()
    {
        string ArriveStation = buyTicketShelfManager.planet_section;
        string LeaveStation = buyTicketShelfManager.GetLeaveStation();
        float TicketPrice = buyTicketShelfManager.price;
        float distance = buyTicketShelfManager.distance;
        float stamina = buyTicketShelfManager.stamina;

        ArriveStationText.text = ArriveStation;
        LeaveStationText.text = LeaveStation;
        TicketPriceText.text = TicketPrice.ToString("F") + " $";
        distanceText.text = distance.ToString("F") + " km.";
        StaminaText.text = stamina.ToString();

    }
}