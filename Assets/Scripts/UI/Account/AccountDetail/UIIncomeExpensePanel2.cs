using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIIncomeExpensePanel2 : MonoBehaviour
{
    public MultiLineChartController multi_chart_controller;
    public TextMeshProUGUI Income_text;
    public TextMeshProUGUI Expense_text;

    public void SetData(List<float>  Income_List, List<float> Expense_List)
    {
        multi_chart_controller.Datas[0].data = Income_List;
        multi_chart_controller.Datas[1].data = Expense_List;

        Income_text.text = $"{Income_List.Sum()}";
        Expense_text.text = $"{Expense_List.Sum()}";
        multi_chart_controller.CreateChart();
    }
}
