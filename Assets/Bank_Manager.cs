using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bank_Manager : MonoBehaviour
{
    public Bank_Status bank_status;
    [SerializeField]
    private Bank_Account_DB.Accounts player_account;
    [SerializeField]
    private Bank_Account_DB DB;
    [SerializeField]
    private Text balance;
    public PlayerStatus player_status;
    private string section_name;

    public Bank_Account_DB.Accounts GetPlayer_Account()
    {
        return this.player_account;
    }

    public void Setplayer_account(float amount_deposits, AccountList account)
    {
        player_account.amount_deposits = amount_deposits;
        player_account.account_list.Insert(0, account);
    }
    private void Awake()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        section_name = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
    }

    private void Update()
    {
        if (!GetPlayerAccount())
        {
            Bank_Account_DB.Accounts newAccount = new Bank_Account_DB.Accounts("xxx",player_status.player_id, "00", 0, section_name, section_name, new List<AccountList>());
            DB.AddNewAccount(newAccount);

            player_account = DB.GetAccount(newAccount.account_id);
        }
        balance.text = player_account.amount_deposits.ToString("F");
    }

    public bool GetPlayerAccount()
    {
        Bank_Account_DB.Accounts account = DB.GetAccount(player_account.account_id);
        if (account != null) {
            player_account = account;
            return true;
        }
        return false;
    }
}
