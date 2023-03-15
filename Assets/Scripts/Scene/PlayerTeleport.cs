using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    
    private GameObject currentTeleporter;
    private bool HaveTicket;
    public bool isOnTeleporter;

    private float teleportTime = 2f;
    private float teleportTimer = 0f;
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

        if (isOnTeleporter)
        {
            teleportTimer += Time.deltaTime;

            if (teleportTimer >= teleportTime)
            {

                if (currentTeleporter != null)
                {
                    if (HaveTicket)
                    {
                        string LeaveStation = gameObject.GetComponent<TicketController>().GetTicket().LeaveStation;
                        if (LeaveStation != "")
                        {
                            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                        }
                    }
                    else
                    {
                        transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                    }
                }
                isOnTeleporter = false;
                teleportTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            HaveTicket = collision.gameObject.GetComponent<Teleporter>().IsHaveTicket;
            isOnTeleporter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                isOnTeleporter = false;
                teleportTimer = 0f;
            }
        }   
    }
}
