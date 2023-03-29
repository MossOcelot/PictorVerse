using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleport : MonoBehaviour
{
    [System.Serializable]
    public class RespanwerData
    {
        public string Scene_name;
        public SceneStatus.section section;
        public Vector3 location;
    }

    [SerializeField]
    private RespanwerData RespawnDefault;
    [SerializeField]
    private List<RespanwerData> RespawnList;

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

    public void Respawner()
    {
        string section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        foreach (RespanwerData respawn in RespawnList)
        {
            string res_section = respawn.section.ToString();
            if (res_section == section)
            {
                SceneManager.LoadScene(respawn.Scene_name);
                transform.position = respawn.location;
                return;
            }
        }

        SceneManager.LoadScene(RespawnDefault.Scene_name);
        transform.position = RespawnDefault.location;


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
