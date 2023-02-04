using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovermentStatus : MonoBehaviour
{
    public enum section { section1, section2, section3, section4, section5 }

    public string name_goverment;
    public section govermentInSection;
    [SerializeField]
    private List<AccountsDetail> govermentAccounts;
    [SerializeField]
    private Financial_Details govermentFinancial;
    [SerializeField]
    private PocketDetails govermentPockets;

    public void addAccountsDetail(AccountsDetail accountDetail)
    {
        this.govermentAccounts.Insert(0, accountDetail);
    }
    public float getGovermentFinancial_balance()
    {
        return govermentFinancial.balance;
    }

    public float getGovermentFinancial_dept()
    {
        return govermentFinancial.debt;
    }
    public void setFinancial_detail(string command, float value)
    {
        if (command == "balance")
        {
            this.govermentFinancial.balance = value;
        }
        else if (command == "debt")
        {
            this.govermentFinancial.debt = value;
        }
    }

}
