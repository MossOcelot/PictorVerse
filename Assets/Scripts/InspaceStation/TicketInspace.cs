using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TicketInspace 
{
    public string ArriveStation;
    public string LeaveStation;
    public string ownerTicket;
    public float ticketprice;
    public float distance;
    public int staminaUsed;
    public bool IsUsed = false;

    public void SetTicket(string newArriveStation, string newLeaveStation, string newownerTicket, float newticketprice, float newdistance, int newstaminaUsed)
    {
        this.ArriveStation = newArriveStation;
        this.LeaveStation = newLeaveStation;
        this.ownerTicket = newownerTicket;
        this.ticketprice = newticketprice;
        this.distance = newdistance;
        this.staminaUsed = newstaminaUsed;
    }

    public void SetIsUsed(bool newIsUsed)
    {
        this.IsUsed = newIsUsed;
    }


}
