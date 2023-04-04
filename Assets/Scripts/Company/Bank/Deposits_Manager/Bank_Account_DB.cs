using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Account_DB : MonoBehaviour
{
    public AccountData accountData;

    private bool checkCustomer_id(int customer_id)
    {
        foreach (AccountData.Accounts account in accountData.accountsDB)
        {
            if(account.customer_id == customer_id) return true;
        }
        return false;
    }

    public bool AddNewAccount(AccountData.Accounts newAccount)
    {
        if (!checkCustomer_id(newAccount.customer_id)) 
        {
            accountData.accountsDB.Add(newAccount);
            return true;
        }
        return false;
    }

    public AccountData.Accounts GetAccount(string account_id)
    {
        foreach(AccountData.Accounts account in accountData.accountsDB)
        {
            if(account_id == account.account_id) return account;
        }
        return null;
    }

    public List<AccountData.Accounts> GetAllAccount()
    {
        return accountData.accountsDB;
    }

    public void LoadAllAccount(List<AccountData.Accounts> data)
    {
        accountData.accountsDB = data;
    }

    public bool UpdateAccount(int command = 0, string account_id = null, int customer_id = 0,
        string account_type = null,float amount_deposits = 0f,string currency_type = null, string place_of_deposit = null, AccountList accountCard = null)
    {
        int account_access = -1;
        int quantity = accountData.accountsDB.Count;
        for(int n = 0; n < quantity; n++)
        {
            if (accountData.accountsDB[n].account_id == account_id)
            {
                account_access = n;
            }
        }
        if (account_access == -1) return false;
        // ปรับรายการ
        if(command == 0)
        {
            if (customer_id == accountData.accountsDB[account_access].customer_id)
            {
                accountData.accountsDB[account_access].amount_deposits = amount_deposits;
                accountData.accountsDB[account_access].account_list.Add(accountCard);
            }
            return true;
        } 
        return false;
    }
}
