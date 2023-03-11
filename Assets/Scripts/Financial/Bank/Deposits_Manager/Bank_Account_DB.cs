using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Account_DB : MonoBehaviour
{
    [System.Serializable]
    public class Accounts
    {
        public string account_id;
        public int customer_id;
        public string account_type;
        public float amount_deposits;
        public string currency_type;
        public string place_of_deposit;

        public List<AccountList> account_list;

        public Accounts (string account_id,int customer_id, string account_type, float amount_deposits, string currency_type, string place_of_deposit, List<AccountList> account_list)
        {
            this.account_id = account_id;
            this.customer_id = customer_id;
            this.account_type = account_type;
            this.amount_deposits = amount_deposits;
            this.currency_type = currency_type;
            this.place_of_deposit = place_of_deposit;
            this.account_list = account_list;
        }   
    }

    [SerializeField]
    private List<Accounts> accountsDB;

    private bool checkCustomer_id(int customer_id)
    {
        foreach (Accounts account in accountsDB)
        {
            if(account.customer_id == customer_id) return true;
        }
        return false;
    }

    public bool AddNewAccount(Accounts newAccount)
    {
        if (!checkCustomer_id(newAccount.customer_id)) 
        {
            this.accountsDB.Add(newAccount);
            return true;
        }
        return false;
    }

    public Accounts GetAccount(string account_id)
    {
        foreach(Accounts account in accountsDB)
        {
            if(account_id == account.account_id) return account;
        }
        return null;
    }

    public List<Accounts> GetAllAccount()
    {
        return this.accountsDB;
    }

    public void LoadAllAccount(List<Accounts> data)
    {
        this.accountsDB = data;
    }

    public bool UpdateAccount(int command = 0, string account_id = null, int customer_id = 0,
        string account_type = null,float amount_deposits = 0f,string currency_type = null, string place_of_deposit = null, AccountList accountCard = null)
    {
        int account_access = -1;
        int quantity = accountsDB.Count;
        for(int n = 0; n < quantity; n++)
        {
            if (accountsDB[n].account_id == account_id)
            {
                account_access = n;
            }
        }
        if (account_access == -1) return false;
        // ปรับรายการ
        if(command == 0)
        {
            if (customer_id == accountsDB[account_access].customer_id)
            {
                accountsDB[account_access].amount_deposits = amount_deposits;
                accountsDB[account_access].account_list.Add(accountCard);
            }
            return true;
        } 
        return false;
    }
}
