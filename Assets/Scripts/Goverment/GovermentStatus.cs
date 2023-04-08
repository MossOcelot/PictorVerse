using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovermentStatus : MonoBehaviour
{
    public GovermentStatusData goverment;
    public void addAccountsDetail(AccountsDetail accountDetail)
    {
        goverment.govermentAccounts.Insert(0, accountDetail);
    }
    public float getGovermentFinancial_balance()
    {
        return goverment.govermentFinancial.balance;
    }

    public float getGovermentFinancial_dept()
    {
        return goverment.govermentFinancial.debt;
    }
    public void setFinancial_detail(string command, float value)
    {
        if (command == "balance")
        {
            goverment.govermentFinancial.balance = value;
        }
        else if (command == "debt")
        {
            goverment.govermentFinancial.debt = value;
        }
    }

    public float GetTaxIncome()
    {
        float AllIncome = 0;
        foreach(AccountsDetail account in goverment.govermentAccounts)
        {
            AllIncome += account.income;
        }
        return AllIncome;
    }
}
