using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIIncomeExpensePie : MonoBehaviour
{
    public PieChart pie_chart;

    public void SetData(List<float> income, List<float> expense)
    {
        float all_income = income.Sum();
        float all_expense = expense.Sum();
        pie_chart.values[0] = all_income;
        pie_chart.values[1] = all_expense;
    }
}
