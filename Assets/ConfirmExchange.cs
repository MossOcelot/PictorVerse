using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmExchange : MonoBehaviour
{
    public ExchangeManager exchangeManager;
    public Text[] cashs;

    public InputField cashInput;
    public TMPro.TMP_Dropdown fromDropBox;
    public TMPro.TMP_Dropdown toDropBox;

    Button exchangeBtn;

    private void Start()
    {
        exchangeBtn = gameObject.GetComponent<Button>();
        exchangeBtn.AddEventListener(cashs, OnClickExchange);
    }

    void OnClickExchange(Text[] cashs)
    {

        float fromCurrency = float.Parse(cashs[0].text);
        float toCurrency = float.Parse(cashs[1].text);

        PlayerStatus player_status = exchangeManager.player;
        float cash_CurrencyPlayerHasExchange = player_status.player_accounts.getPocket()[checkTypeCurrency(fromDropBox.value)];

        bool status_exchange = checkCashInPocket(fromCurrency, cash_CurrencyPlayerHasExchange);
        if (status_exchange)
        {
            // player
            float newValue_from = cash_CurrencyPlayerHasExchange - fromCurrency;
            player_status.player_accounts.setPocket(checkTypeCurrency(fromDropBox.value), newValue_from);

            float newValue_to = player_status.player_accounts.getPocket()[checkTypeCurrency(toDropBox.value)] + toCurrency;
            player_status.player_accounts.setPocket(checkTypeCurrency(toDropBox.value), newValue_to);

            // central bank
            CentralBankStatus bank_status = exchangeManager.centralBankStatus;

            float newValue_expense = bank_status.foreignExchangeReserves.getPocket()[checkTypeCurrency(toDropBox.value)] - toCurrency;
            bank_status.foreignExchangeReserves.setPocket(checkTypeCurrency(toDropBox.value), newValue_expense);

            float newValue_income = bank_status.foreignExchangeReserves.getPocket()[checkTypeCurrency(fromDropBox.value)] + fromCurrency;
            bank_status.foreignExchangeReserves.setPocket(checkTypeCurrency(fromDropBox.value), newValue_income);

            // crate accounts for player

            // get date time
            int[] dateTime = GetDateTime();
            SceneStatus scene_Status = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
            SceneStatus.section fromSection = scene_Status.CheckSectionString(checkTypeCurrency(fromDropBox.value));
            SceneStatus.section toSection = scene_Status.CheckSectionString(checkTypeCurrency(toDropBox.value));
            AccountsDetail account_player = new AccountsDetail() { date = dateTime, accounts_name = "exchange cash", account_type = "EX", income = toCurrency, expense = fromCurrency, currencyIncome_Type = toSection, currencyExpense_Type = fromSection };
            player_status.addAccountsDetails(account_player);

            // create accounts for central bank
            AccountsDetail account_centralBank = new AccountsDetail() { date = dateTime, accounts_name = "exchange cash", account_type = "EX", income = fromCurrency, expense = toCurrency, currencyIncome_Type = fromSection, currencyExpense_Type = toSection };
            bank_status.addBankAccounts(account_centralBank);

            Debug.Log(cashInput.text);
            cashInput.text = "0";
            cashs[1].text = "0";
            return;
        }

        // reset value

        ///// รอเพิ่ม notification แจ้งเตือน
        cashInput.text = "0";
        cashs[1].text = "0";
        Debug.Log("Out cash");

    }
    int[] GetDateTime()
    {
        Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        return date.getDateTime();
    }

    bool checkCashInPocket(float fromCurrency, float cash_CurrencyPlayerHasExchange)
    {
        
        if (fromCurrency <= cash_CurrencyPlayerHasExchange)
        {
            // can exchange
            return true;
        }
        // cant exchange
        return false;
    }

    string checkTypeCurrency(int currency_index)
    {
        if (currency_index == 0)
        {
            return "section1";
        }
        else if (currency_index == 1)
        {
            return "section2";
        }
        else if (currency_index == 2)
        {
            return "section3";
        }
        else if (currency_index == 3)
        {
            return "section4";
        }
        else if (currency_index == 4)
        {
            return "section5";
        }
        else
        {
            return "gold";
        }
    }
}
