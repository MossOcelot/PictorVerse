using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateStatData : MonoBehaviour
{
    [SerializeField]
    private UI_StatusRadarChart uiStatsRadarChart;

    public TextMeshProUGUI credibilityTxt;
    public TextMeshProUGUI stabilityTxt;
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI happinessTxt;
    public TextMeshProUGUI riskTxt;

    private PlayerStatus playerStatus;
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }

    public void Update()
    {
        PlayerStatus.StaticValue player_static = playerStatus.getMyStatic();
        int credit = Mathf.RoundToInt(player_static.static_credibility);
        int stability = Mathf.RoundToInt(player_static.static_stability);
        int health = Mathf.RoundToInt(player_static.static_healthy);
        int happiness = Mathf.RoundToInt(player_static.static_happy);
        int risk = Mathf.RoundToInt(player_static.static_risk);

        UpdateTxt(credit, stability, health, happiness, risk);
        Stats stats = new Stats(credit, stability, health, happiness, risk);
        uiStatsRadarChart.SetStats(stats);
    }

    public void UpdateTxt(int credit, int stability, int health, int happiness, int risk)
    {
        credibilityTxt.text = credit.ToString();
        stabilityTxt.text = stability.ToString();
        healthTxt.text = health.ToString();  
        happinessTxt.text = happiness.ToString();
        riskTxt.text = risk.ToString();
    }
}
