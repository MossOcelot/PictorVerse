using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmWithDraw : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager bank_manager;
    [SerializeField]
    private MyPocketUI Player_pocket;
    [SerializeField]
    private GameObject InputWithDraw;
    [SerializeField]
    private Button WithDrawBtn;

    private float WithDraw_amount;
    private SceneStatus.section section_name;
    private PlayerStatus playerStatus;
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        section_name = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }
    private void Update()
    {
        if (WithDraw_amount > bank_manager.GetPlayer_Account().amount_deposits || WithDraw_amount == 0)
        {
            WithDrawBtn.interactable = false;
        }
        else
        {
            WithDrawBtn.interactable = true;
        }
    }

    public void ConfirmWithDrawal()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
        float newAmount = Player_pocket.GetPocketDetails().getPocket()[section_name.ToString()] + WithDraw_amount;
        Player_pocket.GetPocketDetails().setPocket(section_name.ToString(), newAmount);

        float newWithDraw = bank_manager.GetPlayer_Account().amount_deposits - WithDraw_amount;
        AccountList account = new AccountList(present_date, "withDraw", 0, WithDraw_amount, newWithDraw, "TRW/XW");
        bank_manager.Setplayer_account(newWithDraw, account);

        float credit = WithDraw_amount / 10000f;
        float newCredit = playerStatus.getMyStatic().static_credibility - credit;
        playerStatus.setMyStatic(7, newCredit);

        float newDept = bank_manager.bank_status.GetBank_Financial("debt") - WithDraw_amount;
        bank_manager.bank_status.SetBank_Financial("debt", newDept);

        // player account list
        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "withDraw money", account_type = "withDraw", income = WithDraw_amount, expense = 0 };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank account list
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "accepting withDraw money", account_type = "accepting withDraw", income = WithDraw_amount, expense = 0 };
        bank_manager.bank_status.AddBank_Account(newBankAccountDetail);

        float exchangeCashToGold = new ExchangeRate().getExchangeRate((int)section_name, 0) * WithDraw_amount;
        float newbalance = playerStatus.financial_detail.balance - exchangeCashToGold;
        playerStatus.setFinancial_detail("balance", newbalance);

        // reset data 
        WithDraw_amount = 0f;
        InputWithDraw.gameObject.GetComponent<InputField>().text = "0";
    }

    public void SetInput()
    {
        if (float.TryParse(InputWithDraw.gameObject.GetComponent<InputField>().text, out WithDraw_amount))
        {
            WithDraw_amount = float.Parse(InputWithDraw.gameObject.GetComponent<InputField>().text);
        }
    }
}
