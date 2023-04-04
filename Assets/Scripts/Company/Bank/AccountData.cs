using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AccountData", menuName = "Bank/AccountData")]
public class AccountData : ScriptableObject
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

        public Accounts(string account_id, int customer_id, string account_type, float amount_deposits, string currency_type, string place_of_deposit, List<AccountList> account_list)
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

    public List<Accounts> accountsDB;
    public float InterestRate;
    public int[] InterestDate;
    public bool IsActive = false;
}
