using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Status : MonoBehaviour
{
    public GameObject Banker;
    [SerializeField]
    private string bank_name;
    public PocketDetails Bank_Pocket;
    [SerializeField]
    private List<AccountsDetail> bank_accounts;
    [SerializeField]
    private Financial_Details bank_financial;

    public PocketDetails GetBankPocket()
    {
        return this.Bank_Pocket;
    }

    public void SetBankPocket(PocketDetails pocket)
    {
        this.Bank_Pocket = pocket;
    }

    public void LoadBankAccounts(List<AccountsDetail> data)
    {
        this.bank_accounts = data;
    }
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

    public Financial_Details GetBankAllFinancial()
    {
        return this.bank_financial;
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

    public string GetBank_name()
    {
        return this.bank_name;
    }

    public void SetBank_name(string bankname)
    {
        this.bank_name = bankname;
    }

    public GameObject sendDataBanker()
    {
        return Banker;
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (Banker != null )
        {
            Banker.gameObject.GetComponent<NPCController>().playerIsClose = true;
        }
    }
}
