using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    public List<Asset> assets;
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

    public int GetFinancial_balance()
    {
        return this.financial_detail.balance;
    }

    public int GetFinancial_debt()
    {
        return this.financial_detail.debt;
    }
}
