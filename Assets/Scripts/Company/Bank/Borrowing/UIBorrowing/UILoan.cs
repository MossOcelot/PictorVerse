using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILoan : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager bank_manager;
    private string section;
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
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
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
        loanInterestText.text = "�͡�����Թ��� " + loanInterest.ToString("F");
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

        float newValue = manager.playerStatus.player_accounts.getPocket()[section] + BorrowAmount;
        manager.playerStatus.player_accounts.setPocket(section, newValue);
        
        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "����Թ", account_type = "Loan", income = BorrowAmount, expense = 0 };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank
        float newAmount = bank_manager.bank_status.companyData.pocketCompany.getPocket()[section] - BorrowAmount;
        bank_manager.bank_status.companyData.pocketCompany.setPocket(section, newAmount);
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "����¡��", account_type = "Loan", income = 0, expense = BorrowAmount };
        bank_manager.bank_status.AddBank_Account(newBankAccountDetail);
        if (player.timer[1] + 1 > 12)
        {
            player.timer = new int[] { player.timer[0], 1, player.timer[2] + 1 };
        } else
        {
            Debug.Log("Borrow");
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
