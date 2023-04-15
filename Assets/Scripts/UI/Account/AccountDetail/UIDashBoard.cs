using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDashBoard : MonoBehaviour
{
    public List<float> RI;
    public List<float> FI;
    public List<float> MI;
    public List<float> expense;
    public List<float> insurance1;
    public List<float> insurance2;

    [SerializeField]
    private UIIncomeZonePanel1 inzp1;
    [SerializeField]
    private UIExpenseZonePanel1 epzp1;
    [SerializeField]
    private UIIncomeExpensePanel2 inepp2;
    [SerializeField]
    private UIIncomeExpensePie ineppie;
    [SerializeField]
    private UIInsurancePanel insurancePanel;
    public void SetData()
    {
        inzp1.SetData(RI, FI, MI);
        epzp1.SetData(expense);

        List<float> sum_income = new List<float>();
        int len = RI.Count;
        for(int i = 0; i < len; i++)
        {
           float sum_i = RI[i] + FI[i] + MI[i];
           sum_income.Add(sum_i);
        }
        inepp2.SetData(sum_income, expense);
        ineppie.SetData(sum_income, expense);
        insurancePanel.SetData(insurance1, insurance2);
    }
}
