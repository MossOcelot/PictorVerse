using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIAccountDetailCard : MonoBehaviour
{
    public TextMeshProUGUI date_text;
    public TextMeshProUGUI time_text;
    public TextMeshProUGUI detail_text;
    public TextMeshProUGUI income_text;
    public TextMeshProUGUI expense_text;
    public TextMeshProUGUI other_text;

    public void SetData(int[] date, string detail, float income, float expense, string other)
    {
        date_text.text = date[0].ToString("00") + "/" + date[1].ToString("00") + "/" + date[2].ToString("00");
        time_text.text = date[0].ToString("00") + ":" + date[0].ToString("00");
        detail_text.text = detail;
        income_text.text = income.ToString("F");
        expense_text.text = expense.ToString("F");
        other_text.text = other;
    }
}
