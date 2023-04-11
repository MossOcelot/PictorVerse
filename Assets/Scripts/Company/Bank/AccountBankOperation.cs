using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountBankOperation : MonoBehaviour
{
    [SerializeField]
    private List<AccountData> accountData;

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
}
