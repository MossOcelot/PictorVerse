using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPayLoan : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager bank_manager;
    private string section;
    public BorrowingManager manager;
    private LoanPlayerController loanPlayerController;
    public TextMeshProUGUI LoanAmount;
    public TextMeshProUGUI LoanInterest;
    public TextMeshProUGUI sumLoan;

    public GameObject InputSpendLoan;
    public TextMeshProUGUI TrueLoan;
    public TextMeshProUGUI Interest;
    public TextMeshProUGUI balance;

    public Button SpendBtn;
    [SerializeField]
    private float SpendLoanAmount;
    public float percentSpendInterest;

    public float totalLoan;

    public float leftLoan;
    public float leftInterest;

    public float dept;
    public float loanInterest;

    public float BalanceValue;

    Timesystem time_system;
    private void Start()
    {
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        loanPlayerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<LoanPlayerController>();
        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.AddComponent<Timesystem>();
    }


    private void Update()
    {
        dept = loanPlayerController.GetDept();
        loanInterest = loanPlayerController.GetloanInterest();
        LoanAmount.text = $"{dept}";
        LoanInterest.text = $"{loanInterest}";
        sumLoan.text = $"{dept + loanInterest}";
        totalLoan = dept + loanInterest;

        BalanceValue = (dept + loanInterest) - (leftInterest + leftLoan);
        balance.text = BalanceValue.ToString("F");
    }
    private void FixedUpdate()
    {
        int[] date = time_system.getDateTime();
        LoanPlayerController player = manager.playerStatus.loanPlayerController;

        int len = player.timer.Length;
        if (date[2] < player.timer[2] || player.timer[2] == 0) { Debug.Log("A"); SpendBtn.interactable = false; return; };
        if (date[1] < player.timer[1] || player.timer[1] == 0) { Debug.Log("B"); SpendBtn.interactable = false; return; };
        if (date[0] < player.timer[0] || player.timer[0] == 0) { Debug.Log("C"); SpendBtn.interactable = false; return; };

        if (SpendLoanAmount > totalLoan || SpendLoanAmount <= 0)
        {
            SpendBtn.interactable = false;
        }
        else
        {
            SpendBtn.interactable = true;
        }
    }

    public void SetInput()
    {
        if (float.TryParse(InputSpendLoan.gameObject.GetComponent<TMP_InputField>().text, out SpendLoanAmount))
        {
            SpendLoanAmount = float.Parse(InputSpendLoan.gameObject.GetComponent<TMP_InputField>().text);
            if(SpendLoanAmount * 0.8f >= loanInterest)
            {
                leftInterest = loanInterest;
                leftLoan = SpendLoanAmount - leftInterest;
            } else
            {
                leftInterest = SpendLoanAmount * 0.8f;
                leftLoan = SpendLoanAmount - leftInterest;
            }
            TrueLoan.text = leftLoan.ToString("F");
            Interest.text = leftInterest.ToString("F");
        }
    }

    public void ConfirmSpendLoan()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime();
        LoanPlayerController player = manager.playerStatus.loanPlayerController;
        float newDebt = player.GetDept() - leftLoan;
        float newInterest = player.GetloanInterest() - leftInterest;
        player.SetDebt(newDebt);
        player.SetloanInterest(newInterest);

        float newDeptStatus = manager.playerStatus.financial_detail.debt - SpendLoanAmount;
        manager.playerStatus.financial_detail.debt = newDeptStatus;

        float newValue = manager.playerStatus.player_accounts.getPocket()[section] - SpendLoanAmount;
        manager.playerStatus.player_accounts.setPocket(section, newValue);

        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "¨èÒÂà§Ô¹¡Ùé", account_type = "Loan", income = 0, expense = SpendLoanAmount };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank
        float newAmount = bank_manager.bank_status.Bank_Pocket.getPocket()[section] + SpendLoanAmount;
        bank_manager.bank_status.Bank_Pocket.setPocket(section, newAmount);
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "à§Ô¹¡Ùé", account_type = "Loan", income = SpendLoanAmount, expense = 0 };
        bank_manager.bank_status.AddBank_Account(newBankAccountDetail);

        if (BalanceValue == 0)
        {
            player.HaveDept = false;
        }
        if (player.timer[1] + 1 > 12)
        {
            player.timer = new int[] { player.timer[0], 1, player.timer[2] + 1 };
        } else
        {
            player.timer = new int[] { player.timer[0], player.timer[1] + 1, player.timer[2] };
        }
        resetData();
    }

    void resetData()
    {
        SpendLoanAmount = 0;
        BalanceValue = 0;
        InputSpendLoan.gameObject.GetComponent<TMP_InputField>().text = "0";
    }
}
