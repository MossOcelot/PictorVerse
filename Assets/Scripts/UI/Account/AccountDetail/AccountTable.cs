using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountTable : MonoBehaviour
{
    public GameObject AccountCard_template;
    GameObject AccountCard;
    public Transform content_table;

    private PlayerStatus playerStatus;

    int oldCount;
    public bool showIncome;
    public bool showExpense;
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        List<AccountsDetail> list = playerStatus.getAccountsDetails();
        int len = list.Count;
        if (len != oldCount) 
        {
            ClearTable();
            foreach(AccountsDetail account in list)
            {
                if (showIncome)
                {
                    if(account.income > 0)
                    {
                        AccountCard = Instantiate(AccountCard_template, content_table);
                        UIAccountDetailCard uiCard = AccountCard.GetComponent<UIAccountDetailCard>();
                        uiCard.SetData(account.date, account.accounts_name, account.income, account.expense, account.account_type);
                    }
                };

                if(showExpense)
                {
                    if(account.expense > 0) 
                    {
                        AccountCard = Instantiate(AccountCard_template, content_table);
                        UIAccountDetailCard uiCard = AccountCard.GetComponent<UIAccountDetailCard>();
                        uiCard.SetData(account.date, account.accounts_name, account.income, account.expense, account.account_type);
                    }
                }
            }
        }
    }

    private void ClearTable()
    {
        int count = content_table.childCount;
        if (count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                Destroy(content_table.GetChild(i).gameObject);
            }
        }
    }
}
