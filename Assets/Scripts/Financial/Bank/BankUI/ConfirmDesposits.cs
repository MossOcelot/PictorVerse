using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDesposits : MonoBehaviour
{
    
    [SerializeField]
    private Bank_Manager bank_manager;
    [SerializeField]
    private MyPocketUI Player_pocket;
    [SerializeField]
    private GameObject InputDeposit;
    [SerializeField]
    private Button depositBtn;

    private float Deposit_amount;
    private PlayerStatus playerStatus;
    private SceneStatus.section section_name;
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();   
        section_name = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }
    private void Update()
    {
        if(Deposit_amount > Player_pocket.GetPocketDetails().getPocket()[section_name.ToString()] || Deposit_amount == 0)
        {
            depositBtn.interactable = false;
        } else
        {
            depositBtn.interactable = true;
        }
    }

    public void ConfirmDoposit()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
        float newAmount = Player_pocket.GetPocketDetails().getPocket()[section_name.ToString()] - Deposit_amount;
        Player_pocket.GetPocketDetails().setPocket(section_name.ToString(), newAmount);

        float newDeposit = bank_manager.GetPlayer_Account().amount_deposits + Deposit_amount;
        AccountList account = new AccountList(present_date, "deposit", Deposit_amount, 0, newDeposit, "DEP/PC/CD");
        bank_manager.Setplayer_account(newDeposit, account);

        float newDept = bank_manager.bank_status.GetBank_Financial("debt") + Deposit_amount;
        bank_manager.bank_status.SetBank_Financial("debt", newDept);

        // player account list
        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "deposit money", account_type = "deposit", income = 0, expense = Deposit_amount };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank account list
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "accepting deposits money", account_type = "accepting deposits", income = 0, expense = Deposit_amount };
        bank_manager.bank_status.AddBank_Account(newBankAccountDetail);

        float exchangeCashToGold = new ExchangeRate().getExchangeRate((int)section_name, 0) * Deposit_amount;
        float newbalance = playerStatus.financial_detail.balance + exchangeCashToGold;
        playerStatus.setFinancial_detail("balance", newbalance);

        // reset data
        Deposit_amount = 0f;
        InputDeposit.gameObject.GetComponent<InputField>().text = "0";
    }

    public void SetInput()
    {
        if (float.TryParse(InputDeposit.gameObject.GetComponent<InputField>().text, out Deposit_amount))
        {
            Deposit_amount = float.Parse(InputDeposit.gameObject.GetComponent<InputField>().text);
        }
    }
}
