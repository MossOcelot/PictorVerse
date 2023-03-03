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
        Debug.Log("A");
        if (present_time[0] == InterestDate[0] && present_time[1] == InterestDate[1])
        {
            Debug.Log("1");
            if (!IsActive)
            {
                Debug.Log("2");
                foreach (Bank_Account_DB.Accounts account in db.GetAllAccount())
                {
                    Debug.Log("Hello");
                    float interest_value = account.amount_deposits * (bank_policy.GetInterestRate() / 100);
                    account.amount_deposits += interest_value;

                    AccountList account_card = new AccountList(present_time, "¥Õ°‡∫’È¬‡ß‘πΩ“°", interest_value, 0, account.amount_deposits, "INT/IN");
                    account.account_list.Insert(0, account_card);
                }
                IsActive= true;
            }
        } else
        {
            IsActive= false;
        }
    }
}
