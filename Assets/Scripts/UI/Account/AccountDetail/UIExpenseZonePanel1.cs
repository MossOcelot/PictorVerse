using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIExpenseZonePanel1 : MonoBehaviour
{
    public MultiLineChartController multi_chart_controller;
    public TextMeshProUGUI Expense_text;

    public void SetData(List<float> Expense_List)
    {
        multi_chart_controller.Datas[0].data = Expense_List;

        Expense_text.text = $"{Expense_List.Sum()}";
        multi_chart_controller.CreateChart();
    }
}
