using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCompanyStatus : MonoBehaviour
{
    public CompanyData companyData;

    int section;
    public void Awake()
    {
        section = (int)GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }

    private void Update()
    {
        float total = 0;
        int n = 0;
        foreach(string key in companyData.pocketCompany.getPocket().Keys)
        {
            if(n != section)
            {
                float exchangeMoney = new ExchangeRate().getExchangeRate(n, section) * companyData.pocketCompany.getPocket()[key];
                total += exchangeMoney;
            } else
            {
                total += companyData.pocketCompany.getPocket()[key];
            }
            n++;
        }
        companyData.financialDetail.balance = total;
    }
    public List<AccountsDetail> getAccountsDetails()
    {
        return companyData.accountDetailCompany;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        companyData.accountDetailCompany.Insert(0, account);
    }
} 
