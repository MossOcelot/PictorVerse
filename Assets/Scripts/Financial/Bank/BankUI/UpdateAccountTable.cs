using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAccountTable : MonoBehaviour
{
    [SerializeField] private Bank_Manager bank_manager;
    [SerializeField] private GameObject table;

    [SerializeField] private GameObject cell_template;
    GameObject cell;
    // Update is called once per frame
    void FixedUpdate()
    {
        List<AccountList> acount_list = bank_manager.GetPlayer_Account().account_list;

        int len = table.transform.childCount;
        if(len > 0)
        {
            for(int i = 0; i < len; i++)
            {
                Destroy(table.transform.GetChild(i).gameObject);
            }
        }
        foreach (AccountList account in acount_list)
        {
            cell = Instantiate(cell_template, table.transform);
            cell.gameObject.GetComponent<CellTableUI>().SetData(account.date, account.accounts_name, account.deposit, account.withdrawal, account.number);
        }

    }
}
