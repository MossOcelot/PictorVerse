using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyPocketUI : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager player;

    [SerializeField]
    private Text money_section1;
    [SerializeField]
    private Text money_section2;
    [SerializeField]
    private Text money_section3;
    [SerializeField] 
    private Text money_section4;
    [SerializeField] 
    private Text money_section5;

    [SerializeField]
    private PocketDetails player_pocket;

    public PocketDetails GetPocketDetails()
    {
        return this.player_pocket;
    }
    private void FixedUpdate()
    {
        player_pocket = player.player_status.player_accounts;
        money_section1.text = player_pocket.getPocket()["section1"].ToString("F");
        money_section2.text = player_pocket.getPocket()["section2"].ToString("F");
        money_section3.text = player_pocket.getPocket()["section3"].ToString("F");
        money_section4.text = player_pocket.getPocket()["section4"].ToString("F");
        money_section5.text = player_pocket.getPocket()["section5"].ToString("F");
    }
}
