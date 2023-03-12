using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Account_manager : MonoBehaviour
{
    [SerializeField]
    private Bank_Policy bank_policy;
    [SerializeField]
    private Bank_Account_DB db;
    [SerializeField]
    private int[] InterestDate;
    [SerializeField]
    private bool IsActive = false;
    private void FixedUpdate()
    {
        int[] present_time = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
        if (present_time[0] == InterestDate[0] && present_time[1] == InterestDate[1])
        {
            if (!IsActive)
            {
                foreach (Bank_Account_DB.Accounts account in db.GetAllAccount())
                {
                    Debug.Log("Hello");
                    float interest_value = account.amount_deposits * (bank_policy.GetInterestRate() / 100);
                    account.amount_deposits += interest_value;

                    AccountList account_card = new AccountList(present_time, "interest", interest_value, 0, account.amount_deposits, "INT/IN");
                    account.account_list.Insert(0, account_card);
                }
                IsActive= true;
            }
        } else
        {
            IsActive= false;
        }
    }

    public void LoadInterestDate(int[] data)
    {
        this.InterestDate = data;
    }

    public int[] GetInterestDate()
    {
        return this.InterestDate;
    }

    public void LoadIsActive(bool data)
    {
        this.IsActive = data;
    }

    public bool GetIsActive()
    {
        return this.IsActive;
    }
}
