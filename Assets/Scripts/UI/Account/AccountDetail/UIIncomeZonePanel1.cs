using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIIncomeZonePanel1 : MonoBehaviour
{
    public MultiLineChartController multi_chart_controller;
    public TextMeshProUGUI RI_text;
    public TextMeshProUGUI FI_text;
    public TextMeshProUGUI MI_text;

    public void SetData(List<float> RI_List, List<float> FI_List, List<float> MI_List)
    {
        multi_chart_controller.Datas[0].data = RI_List;
        multi_chart_controller.Datas[1].data = FI_List;
        multi_chart_controller.Datas[2].data = MI_List;

        RI_text.text = $"{RI_List.Sum()}";
        FI_text.text = $"{FI_List.Sum()}";
        MI_text.text = $"{MI_List.Sum()}";

        multi_chart_controller.CreateChart();
    }
}
