using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIAccount_Bar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI date;
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI detail;
    [SerializeField]
    private TextMeshProUGUI income;
    [SerializeField]
    private TextMeshProUGUI expense;
    [SerializeField]
    private TextMeshProUGUI status;

    public void SetData(int[] Date, string name_detail, float income, float expense, string status)
    {
        this.date.text = Date[0].ToString("00") + "/" + Date[1].ToString("00") + "/" + Date[2].ToString("00");
        this.time.text = Date[3].ToString("00") + ":" + Date[4].ToString("00");
        this.detail.text = name_detail;
        this.income.text = income.ToString("F");
        this.expense.text = expense.ToString("F");
        this.status.text = status;
    }
}
