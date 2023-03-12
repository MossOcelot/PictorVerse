using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCompanyStatus : MonoBehaviour
{
    public string nameCompany;
    public PocketDetails pocketCompany;
    [SerializeField]
    private Financial_Details financialDetail;
    [SerializeField]
    private List<AccountsDetail> accountDetailCompany;

    int section;
    public void Awake()
    {
        section = (int)GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }

    private void Update()
    {
        float total = 0;
        int n = 0;
        foreach(string key in pocketCompany.getPocket().Keys)
        {
            if(n != section)
            {
                float exchangeMoney = new ExchangeRate().getExchangeRate(n, section) * pocketCompany.getPocket()[key];
                total += exchangeMoney;
            } else
            {
                total += pocketCompany.getPocket()[key];
            }
            n++;
        }
        financialDetail.balance = total;
    }
    public List<AccountsDetail> getAccountsDetails()
    {
        return this.accountDetailCompany;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        this.accountDetailCompany.Insert(0, account);
    }
} 
