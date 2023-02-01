using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    class StaticValue
    {
        public int static_useEnergy;
        public int static_spendVAT;
        public int static_spendBuy;
    };

    public int player_id => GetInstanceID();
    [SerializeField]
    private string playername;
    [SerializeField]
    private int HP;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int cash;
    [SerializeField]
    private List<InventoryItem> myBag;
    [SerializeField]
    private List<AccountsDetail> accountsDetails;
    [SerializeField]
    private StaticValue myStatic;
    public void setEnergy(int useEnergy)
    {
        this.energy += useEnergy;
    }

    public int getEnergy()
    {
        return this.energy;
    }

    public void changeCash(int newCash)
    {
        this.cash = newCash;
    }

    public int getCash()
    {
        return this.cash;
    }
    public List<InventoryItem> getItemInBag()
    {
        return this.myBag;
    }
    public void addItemInBag(InventoryItem newitem) 
    { 
        this.myBag.Add(newitem);
    }

    public void setItemInBag(int index, InventoryItem item)
    {
        this.myBag[index] = item;
    }
    public void deleteItemInBag(InventoryItem item)
    {
        this.myBag.Remove(item);
    }
    public Dictionary<string, int> getMyStatic()
    {
        return new Dictionary<string, int>
        {
            {"static_useEnergy", this.myStatic.static_useEnergy },
            {"static_SpendBuy", this.myStatic.static_spendBuy },
            {"static_SpendVat", this.myStatic.static_spendVAT }
        };
    }

    public void setMyStatic(int command, int value)
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
