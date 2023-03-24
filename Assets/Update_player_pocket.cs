using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Linq;

public class Update_player_pocket : MonoBehaviour
{
    public TextMeshProUGUI main_cash;
    public TextMeshProUGUI[] otherCash;

    PlayerStatus player_status;
    PlayerMovement player_movement;
    string SceneStatus;

    public bool status = false;
    private void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        player_movement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
        SceneStatus = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();

    }
    private void FixedUpdate()
    {
        Dictionary<string, float> player_pocket = player_status.player_accounts.getPocket();
        main_cash.text = String.Format("<sprite index={0}>", player_pocket.Values.ToList().IndexOf(player_pocket[SceneStatus])) + player_pocket[SceneStatus].ToString("F");

        int n = 0;
        foreach (string key in player_pocket.Keys)
        {
            if (key == SceneStatus)
            {
                continue;
            }
            otherCash[n].text = String.Format("<sprite index={0}>",n+1) + player_pocket[key].ToString();
            n++;
        }
    }

    public void show()
    {
        status = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        player_movement.isLooking = true;
    }

    public void hide()
    {
        status = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        player_movement.isLooking = false;
    }
}
