using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private UI_StatusRadarChart uiStatsRadarChart;
    private void Start()
    {
        Stats status = new Stats(10, 5, 20, 12, 15);

        uiStatsRadarChart.SetStats(status);
    }
}
