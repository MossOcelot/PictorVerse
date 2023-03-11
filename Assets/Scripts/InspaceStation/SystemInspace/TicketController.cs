using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketController : MonoBehaviour
{
    public PlayerStatus playerStatus;
    [SerializeField]
    private TicketInspace ticket;

    public TicketInspace useTicketInSpace()
    {
        if(ticket != null)
        {
            TicketInspace myticket = ticket;
            ticket = new TicketInspace();
            return  myticket;
        }else
        {
            return null;
        }
    }

    public void AddTicket(TicketInspace newticket)
    {
        this.ticket = newticket;
    }

    public TicketInspace GetTicket()
    {
        return ticket;
    }
}
