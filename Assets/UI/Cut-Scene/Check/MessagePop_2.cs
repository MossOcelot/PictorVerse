using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePop_2 : MonoBehaviour
{
    public GameObject Message;
    public GameObject Map;
    public GameObject Noti;

    public GameObject Statuss;

    public GameObject Mission;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Message.SetActive(true);
            Noti.SetActive(false);
            Mission.SetActive(false);
            Map.SetActive(false);
            Statuss.SetActive(false);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Message.SetActive(false);
            Noti.SetActive(true);
            Mission.SetActive(true);
            Map.SetActive(true);
            Statuss.SetActive(true);
        }
    }
}
