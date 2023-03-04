using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Status : MonoBehaviour
{
    public PocketDetails Bank_Pocket;
    [SerializeField]
    private List<AccountsDetail> bank_accounts;
    [SerializeField]
    private Financial_Details bank_financial;

    public List<AccountsDetail> GetBank_Accounts()
    {
        return this.bank_accounts;
    }
    public void AddBank_Account(AccountsDetail newAccount)
    {
        this.bank_accounts.Insert(0, newAccount);
    }
    public float GetBank_Financial(string command)
    {
        if(command == "balance")
        {
            return this.bank_financial.balance;
        } else if (command == "debt")
        {
            return this.bank_financial.debt;
        }
        return 0;
    }
    public void SetBank_Financial(string command, float amounts)
    {
        if(command == "balance")
        {
            this.bank_financial.balance = amounts;
        } else if (command == "debt")
        {
            this.bank_financial.debt = amounts;
        }
    }
}
