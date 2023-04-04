using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountBankGobalManager : MonoBehaviour
{
    public List<AccountData> accounts;

    private void FixedUpdate()
    {
        foreach(AccountData account in accounts)
        {
            int[] present_time = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
            if (present_time[0] == account.InterestDate[0] && present_time[1] == account.InterestDate[1])
            {
                if (!account.IsActive)
                {
                    foreach (AccountData.Accounts acc in account.accountsDB)
                    {
                        Debug.Log("Hello");
                        float interest_value = acc.amount_deposits * (account.InterestRate / 100);
                        acc.amount_deposits += interest_value;

                        AccountList account_card = new AccountList(present_time, "interest", interest_value, 0, acc.amount_deposits, "INT/IN");
                        acc.account_list.Insert(0, account_card);
                    }
                    account.IsActive = true;
                }
            }
            else
            {
                account.IsActive = false;
            }
        }
    }
}
