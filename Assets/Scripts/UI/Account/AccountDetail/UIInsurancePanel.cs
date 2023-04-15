using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIInsurancePanel : MonoBehaviour
{
    public MultiBarChart multi_chart_controller;

    public TextMeshProUGUI insurance_text1;
    public TextMeshProUGUI insurance_text2;

    public void SetData(List<float> insurance1, List<float> insurance2)
    {
        multi_chart_controller.values1 = insurance1;
        multi_chart_controller.values2 = insurance2;

        insurance_text1.text = $"{insurance1.Sum()}";
        insurance_text2.text = $"{insurance2.Sum()}";

        multi_chart_controller.CreateChart();
    }
}
