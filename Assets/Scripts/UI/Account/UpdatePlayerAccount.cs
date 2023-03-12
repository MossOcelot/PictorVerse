using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerAccount : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player_status;
    [SerializeField]
    private Transform account_list;
    [SerializeField]
    private GameObject Account_Bar_template;

    int last_count;
    GameObject account_card;
    // Update is called once per frame
    void Update()
    {
        List<AccountsDetail> player_account = player_status.getAccountsDetails();

        int len = player_account.Count;
        if(last_count != len)
        {
            last_count = len;

            int amount_bar = account_list.gameObject.transform.childCount;
            for(int n = 0; n < amount_bar; n++)
            {
                Destroy(account_list.gameObject.transform.GetChild(n).gameObject);
            }

            foreach(AccountsDetail account in player_account)
            {
                int[] date = account.date;
                string account_name = account.accounts_name;
                string account_type = account.account_type;
                float income = account.income;
                float expense = account.expense;

                account_card = Instantiate(Account_Bar_template, account_list);
                account_card.gameObject.GetComponent<UIAccount_Bar>().SetData(date, account_name, income, expense, account_type);
            }
        }
    }
}
