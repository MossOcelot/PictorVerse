using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Status : MonoBehaviour
{
    public GameObject Banker;
    [SerializeField]
    public CompanyData companyData;

    public PocketDetails GetBankPocket()
    {
        return companyData.pocketCompany;
    }

    public void SetBankPocket(PocketDetails pocket)
    {
        companyData.pocketCompany = pocket;
    }

    public void LoadBankAccounts(List<AccountsDetail> data)
    {
        companyData.accountDetailCompany = data;
    }
    public List<AccountsDetail> GetBank_Accounts()
    {
        return companyData.accountDetailCompany;
    }
    public void AddBank_Account(AccountsDetail newAccount)
    {
        companyData.accountDetailCompany.Insert(0, newAccount);
    }
    public float GetBank_Financial(string command)
    {
        if(command == "balance")
        {
            return companyData.financialDetail.balance;
        } else if (command == "debt")
        {
            return companyData.financialDetail.debt;
        }
        return 0;
    }

    public Financial_Details GetBankAllFinancial()
    {
        return companyData.financialDetail;
    }
    public void SetBank_Financial(string command, float amounts)
    {
        if(command == "balance")
        {
            companyData.financialDetail.balance = amounts;
        } else if (command == "debt")
        {
            companyData.financialDetail.debt = amounts;
        }
    }

    public string GetBank_name()
    {
        return companyData.nameCompany;
    }

    public void SetBank_name(string bankname)
    {
        companyData.nameCompany = bankname;
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
            Banker.gameObject.GetComponent<NPCController>().IsOpenShelf = false;
        }
    }
}
