using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_player_pocket : MonoBehaviour
{
    public Text main_cash;
    public Text[] otherCash;

    PlayerStatus player_status;
    string SceneStatus;
    private void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        SceneStatus = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
    }
    private void FixedUpdate()
    {
        Dictionary<string, float> player_pocket = player_status.player_accounts.getPocket();
        main_cash.text = player_pocket[SceneStatus].ToString("F");
        

        int n = 0;
        foreach(string key in player_pocket.Keys)
        {
            if (key == SceneStatus)
            {
                continue;
            }
            otherCash[n].text = player_pocket[key].ToString();
            n++;
        }
    }
}
