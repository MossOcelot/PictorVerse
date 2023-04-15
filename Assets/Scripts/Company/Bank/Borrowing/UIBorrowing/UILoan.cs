using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILoan : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager bank_manager;
    private SceneStatus.section section;
    public BorrowingManager manager;
    public float playerHasLoan;

    public TextMeshProUGUI creditText;
    public TextMeshProUGUI loanText;
    public GameObject InputLoan;
    public TextMeshProUGUI loanInterestText;
    public Button BorrowBtn;
    float OldCreditPlayer = -1;

    float loanAmount;
    public float loanAmountTrue;
    public float BorrowAmount;

    private void Start()
    {
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }
    private void Update()
    {
        PlayerStatus player = manager.playerStatus;
        float credit = player.getMyStatic().static_credibility;
        if (OldCreditPlayer != credit)
        {
            if (credit < 100)
            {
                loanAmount = 50000f;
            }
            else
            {
                loanAmount = 50000f + (credit * 5000f);
            }
        }
        playerHasLoan = player.loanPlayerController.SumDept();
        loanAmountTrue = loanAmount - playerHasLoan;

        SetData(credit, loanAmountTrue, 20);
    }

    private void FixedUpdate()
    {
        if (BorrowAmount > loanAmount || BorrowAmount <= 0)
        {
            BorrowBtn.interactable = false;
        }
        else
        {
            BorrowBtn.interactable = true;
        }
    }

    private void SetData(float credit, float loan, float loanInterest)
    {
        creditText.text = credit.ToString("F");    
        loanText.text = loan.ToString("F");
        loanInterestText.text = "´Í¡àºÕéÂà§Ô¹¡Ùé " + loanInterest.ToString("F");
    }

    public void SetInput()
    {
        if (float.TryParse(InputLoan.gameObject.GetComponent<TMP_InputField>().text, out BorrowAmount))
        {
            BorrowAmount = float.Parse(InputLoan.gameObject.GetComponent<TMP_InputField>().text);
        }
    }

    public void ConfirmBorrow()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
        LoanPlayerController player = manager.playerStatus.loanPlayerController;
        float newDebt = player.GetDept() + BorrowAmount;
        player.SetDebt(newDebt);

        float newDeptStatus = manager.playerStatus.financial_detail.debt + BorrowAmount;
        manager.playerStatus.financial_detail.debt = newDeptStatus;

        float newValue = manager.playerStatus.player_accounts.getPocket()[section.ToString()] + BorrowAmount;
        manager.playerStatus.player_accounts.setPocket(section.ToString(), newValue);
        
        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "¡Ùéà§Ô¹", account_type = "LI", income = BorrowAmount, expense = 0, currencyIncome_Type = section, currencyExpense_Type = section };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank
        float newAmount = bank_manager.bank_status.companyData.pocketCompany.getPocket()[section.ToString()] - BorrowAmount;
        bank_manager.bank_status.companyData.pocketCompany.setPocket(section.ToString(), newAmount);
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "»ÅèÍÂ¡Ùé", account_type = "DE", income = 0, expense = BorrowAmount, currencyIncome_Type = section, currencyExpense_Type = section };
        bank_manager.bank_status.AddBank_Account(newBankAccountDetail);
        if (player.timer[1] + 1 > 12)
        {
            player.timer = new int[] { player.timer[0], 1, player.timer[2] + 1 };
        } else
        {
            player.timer = new int[] { present_date[0], present_date[1] + 1, present_date[2] };
        }
        player.HaveDept = true;
        resetData();
    }

    void resetData()
    {
        BorrowAmount = 0;
        InputLoan.gameObject.GetComponent<TMP_InputField>().text = "0";
    }
}
