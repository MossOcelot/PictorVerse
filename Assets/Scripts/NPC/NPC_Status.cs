using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Status : MonoBehaviour
{
    public Sprite npc_img;
    public int npc_id => GetInstanceID();
    public string npc_name;
    [SerializeField]
    private string birthday;
    public string live_place;

    [SerializeField]
    private Financial_Details financial_detail;
    [SerializeField]
    private List<AccountsDetail> accountsDetails;
    public List<Asset> assets;

    private void Awake()
    {
        // get npc live_place
        live_place = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
    }
    public void setFinancial_detail(string command,int value)
    {
        if (command == "balance")
        {
            this.financial_detail.balance = value;
        } else if (command == "debt")
        {
            this.financial_detail.debt = value;
        }
    }

    public float GetFinancial_balance()
    {
        return this.financial_detail.balance;
    }

    public float GetFinancial_debt()
    {
        return this.financial_detail.debt;
    }

    public List<AccountsDetail> getAccountsDetails()
    {
        return this.accountsDetails;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        this.accountsDetails.Insert(0, account);
    }

    
}
