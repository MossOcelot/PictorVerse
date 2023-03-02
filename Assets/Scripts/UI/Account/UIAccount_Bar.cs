using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAccount_Bar : MonoBehaviour
{
    [SerializeField]
    private Text date;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text detail;
    [SerializeField]
    private Text income;
    [SerializeField]
    private Text expense;
    [SerializeField]
    private Text status;

    public void SetData(int[] Date, string name_detail, float income, float expense, string status)
    {
        this.date.text = Date[0].ToString("00") + "/" + Date[1].ToString("00") + "/" + Date[2].ToString("00");
        this.time.text = Date[3].ToString("00") + ":" + Date[4].ToString("00") + ":" + Date[5].ToString("00");
        this.detail.text = name_detail;
        this.income.text = income.ToString("F");
        this.expense.text = expense.ToString("F");
        this.status.text = status;
    }
}
