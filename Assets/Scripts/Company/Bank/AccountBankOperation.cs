using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountBankOperation : MonoBehaviour
{
    [SerializeField]
    private List<AccountData> accountData;

    int old_day;
    public float GetDeposit(string accountID)
    {
        foreach(AccountData data in accountData)
        {
            foreach(AccountData.Accounts account in data.accountsDB)
            {
                if(account.account_id == accountID)
                {
                    return account.amount_deposits;
                }
            }
        }
        return 0;
    }

    public List<AccountData> GetAccountData()
    {
        return accountData;
    }

    public void SetDeposit(string accountID, float newValue)
    {
        foreach (AccountData data in accountData)
        {
            foreach (AccountData.Accounts account in data.accountsDB)
            {
                if (account.account_id == accountID)
                {
                    account.amount_deposits = newValue;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();

        if(old_day != present_date[0]) 
        {
            foreach (AccountData data in accountData)
            {
                int[] date = data.InterestDate;
                if (date[1] == present_date[1])
                {
                    if (date[0] == present_date[0])
                    {
                        foreach(AccountData.Accounts accountsDB in data.accountsDB)
                        {
                            float new_amount = accountsDB.amount_deposits * (1f + (data.InterestRate / 100f));
                            accountsDB.amount_deposits = new_amount;
                        }
                    }
                }
            }
            old_day = present_date[0];
        }
        
    }
}
