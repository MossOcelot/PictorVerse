using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public TextMeshProUGUI text_year;

    public Button PayTaxBtn;
    public TextMeshProUGUI text2;
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
        text1.text = $"�ӹǹ�ѹ����ҧ���� {late_time} �ѹ";

        PayTaxBtn.interactable = false;
        text2.color = Color.white;
        text2.text = "�����͵�Ǩ�ͺ�����š�͹ -->";
    }

    public void SetButton()
    {
        bool CheckInfo = fileTax.GetCheck();
        if (CheckInfo)
        {
            PayTaxBtn.interactable = true;
            text2.color = Color.green;
            text2.text = "�����Ŷ١��ͧ";
        }
        else
        {
            PayTaxBtn.interactable = false;
            text2.color = Color.red;
            text2.text = "���������ç�ѹ�ô��Ѻ����ա����";
        }
        fileTax.FirstChangeT1 = false;
        fileTax.FirstChangeT2 = false;
        fileTax.FirstChangeT3 = false;
        fileTax.isClick1 = true;
        fileTax.isClick2 = true;
    }
}
