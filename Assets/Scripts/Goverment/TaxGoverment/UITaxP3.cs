using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UITaxP3 : MonoBehaviour
{
    [SerializeField]
    private FileTaxSystem fileTax;
    public TextMeshProUGUI text_income;
    public TextMeshProUGUI text_exception;
    public TextMeshProUGUI text_expensetion;
    public TextMeshProUGUI text_deduction;
    public TextMeshProUGUI text_incomeCalcuTax;

    public TextMeshProUGUI text_tax;
    public TextMeshProUGUI text_nettax;

    public TextMeshProUGUI text1;

    public void SetData()
    {
        float allIncome = fileTax.AllIncome();
        float allExpense = fileTax.AllExpense();
        float deduction = fileTax.deduction;
        float incomeCalcuTax = fileTax.IncomeCalTax;
        float netTax = fileTax.netTax;
        int late_time = fileTax.late_day;

        text_income.text = allIncome.ToString("F");
        text_exception.text = "0";
        text_expensetion.text = allExpense.ToString("F");
        text_deduction.text = deduction.ToString("F");
        text_incomeCalcuTax.text = incomeCalcuTax.ToString("F");
        text_tax.text = netTax.ToString("F");
        text_nettax.text = netTax.ToString("F");
        text1.text = $"จำนวนวันที่ค้างชำระ {late_time} วัน";
    }
}
