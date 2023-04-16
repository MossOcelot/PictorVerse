using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIAccountDetail : MonoBehaviour
{
    public GameObject UI;
    public UIDashBoard dashboard;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(UI.activeSelf)
            {
                Hide();
            }
            else
            {
                Open();
            }
        }
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Open()
    {
        UI.SetActive(true);
        GetData();
    }

    private void GetData()
    {
        float[] RI = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] FI = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] MI = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] expense = new float[] { 0, 0, 0, 0, 0, 110, 230, 420, 530, 120, 10, 0 };
        PlayerStatus player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        foreach(AccountsDetail account in player_status.getAccountsDetails())
        {
            if(account.account_type == "RI")
            {
                RI[account.date[1]] += account.income;
            }
            else if(account.account_type == "FI")
            {
                FI[account.date[1]] += account.income;
            }
            else if(account.account_type == "MI")
            {
                MI[account.date[1]] += account.income;
            }
            
            if(account.expense > 0)
            {
                Debug.Log($"Expense {account.date[1]} : {account.expense}");    
                expense[account.date[1]] += account.expense;
            }
        }
        dashboard.RI = RI.ToList();
        dashboard.FI = FI.ToList();
        dashboard.MI = MI.ToList(); 
        dashboard.expense = expense.ToList();

        float[] insurance1 = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] insurance2 = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        InsuranceController insurance_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        foreach (InsuranceData data in insurance_controller.insuranceDataBuy)
        {
            if(data.type_insurance == "Endowment")
            {
                insurance1[data.buyDate[1]] += data.amount;
            }
            else if(data.type_insurance == "Health_insurance")
            {
                insurance2[data.buyDate[1]] += data.amount;
            }
        }

        dashboard.insurance1 = insurance1.ToList();
        dashboard.insurance2 = insurance2.ToList();
            
        dashboard.SetData();
    }

}
