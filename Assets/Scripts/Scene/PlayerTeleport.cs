using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    
    private GameObject currentTeleporter;
    private bool HaveTicket;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(currentTeleporter != null)
            {
                if (HaveTicket)
                {
                    string LeaveStation = gameObject.GetComponent<TicketController>().GetTicket().LeaveStation;
                    if(LeaveStation != "")
                    {
                        transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                    }
                } else
                {
                    transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            HaveTicket = collision.gameObject.GetComponent<Teleporter>().IsHaveTicket;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }   
    }
}
