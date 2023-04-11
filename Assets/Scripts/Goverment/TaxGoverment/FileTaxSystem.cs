using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FileTaxSystem : MonoBehaviour
{
    public float InputRI;
    public float InputFI;
    public float InputMI;

    public float InputEndownment;
    public float InputHeathInsurance;

    public float expensesRI;
    public float expensesFI;
    public float expensesMI;

    public float deduction;

    public float IncomeCalTax;
    public float netTax;
    public int late_day;

    private PlayerStatus player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }
    public float AllIncome()
    {
        return InputRI + InputFI + InputMI;
    }

    public float AllExpense()
    {
        return expensesRI + expensesFI + expensesMI;
    }

    public void CalculateExpense()
    {
        expensesFI = Mathf.RoundToInt(InputFI * 0.5f);
        expensesRI = Mathf.RoundToInt(InputRI * 0.5f);
        expensesMI = Mathf.RoundToInt(InputMI * 0.6f);
    }

    public void CalculateDeduction()
    {
        float allAmount = InputEndownment + InputHeathInsurance;
        if(allAmount > 100000)
        {
            deduction = 100000;
        } else
        {
            deduction = allAmount;
        }
    }

    public void CalculateIncomeCalTax()
    {
        float allIncome = AllIncome();
        float allExpense = AllExpense();
        IncomeCalTax = allIncome - allExpense - deduction;
    }

    public void CalNetTax()
    {
       netTax = player.PayTaxes(IncomeCalTax);
    }
}
