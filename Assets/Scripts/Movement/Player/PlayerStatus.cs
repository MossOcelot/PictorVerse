using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;
public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    class StaticValue
    {
        public float static_useEnergy;
        public float static_spendVAT;
        public float static_spendBuy;
    };

    public int player_id => GetInstanceID();
    [SerializeField]
    private string playername;
    [SerializeField]
    public PocketDetails player_accounts;
    [SerializeField]
    private Financial_Details financial_detail;
    [SerializeField]
    private List<AccountsDetail> accountsDetails;
    [SerializeField]
    private StaticValue myStatic;
    [SerializeField]
    private string account_id;

    [SerializeField] 
    private int MaxHP;
    [SerializeField]
    private int MaxEnergy;
    [SerializeField]
    private int HP;
    [SerializeField]
    private int energy;

    public bool IsDead = false;

    public void setMaxHP(int MaxHP)
    {
        this.MaxHP += MaxHP;
    }
    public int getMaxHP()
    {
        return this.MaxHP;
    }
    public void setMaxEnergy(int MaxEnergy)
    {
        this.MaxEnergy += MaxEnergy;
    }

    public int getMaxEnergy()
    {
        return this.MaxEnergy;
    }

    public void setHP(int hp)
    {
        this.HP += hp;
    }
    public int getHP()
    {
        return this.HP;
    }
    public void setEnergy(int useEnergy)
    {
        this.energy += useEnergy;
    }

    public int getEnergy()
    {
        return this.energy;
    }

    public Dictionary<string, float> getMyStatic()
    {
        return new Dictionary<string, float>
        {
            {"static_useEnergy", this.myStatic.static_useEnergy },
            {"static_SpendBuy", this.myStatic.static_spendBuy },
            {"static_SpendVat", this.myStatic.static_spendVAT }
        };
    }

    public void setMyStatic(int command, float value)
    {
        if (command == 0)
        {
            this.myStatic.static_useEnergy = value;
        } else if (command == 1)
        {
            this.myStatic.static_spendBuy = value;
        } else if (command == 2)
        {
            this.myStatic.static_spendVAT = value;
        }
    }

    public List<AccountsDetail> getAccountsDetails()
    {
        return this.accountsDetails;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        this.accountsDetails.Insert(0, account);
    }

    public void SetAccountID(string account_id)
    {
        this.account_id = account_id;
    }
    public string GetAccountID()
    {
        return this.account_id;
    }

    public void setFinancial_detail(string command, int value)
    {
        if (command == "balance")
        {
            this.financial_detail.balance = value;
        }
        else if (command == "debt")
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

    public bool GetIsDead()
    {
        return this.IsDead;
    }

    public void SetIsdead(bool isdead)
    {
        this.IsDead = isdead;
    }

    private void Update()
    {
        if (this.energy <= 0) {
            Debug.Log("Empty Energy");
            this.energy = 0;
        }
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
