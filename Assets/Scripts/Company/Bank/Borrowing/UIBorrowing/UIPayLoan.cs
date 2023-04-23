using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPayLoan : MonoBehaviour
{
    [SerializeField]
    private Bank_Manager bank_manager;
    private SceneStatus.section section;
    public BorrowingManager manager;
    private LoanPlayerController loanPlayerController;
    public TextMeshProUGUI LoanAmount;
    public TextMeshProUGUI LoanInterest;
    public TextMeshProUGUI sumLoan;

    public GameObject InputSpendLoan;
    public TextMeshProUGUI TrueLoan;
    public TextMeshProUGUI Interest;
    public TextMeshProUGUI balance;
    public TextMeshProUGUI LimitPay;
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
    public float limit_pay;
    Timesystem time_system;
    private void Start()
    {
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
        loanPlayerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<LoanPlayerController>();
        time_system = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
    }


    private void Update()
    {
        dept = loanPlayerController.GetDept();
        loanInterest = loanPlayerController.GetloanInterest();
        LoanAmount.text = $"{dept}";
        LoanInterest.text = $"{loanInterest}";
        sumLoan.text = $"{dept + loanInterest}";
        totalLoan = dept + loanInterest;
        limit_pay = (totalLoan * 0.10f) * (float)loanPlayerController.round;
        BalanceValue = (dept + loanInterest) - (leftInterest + leftLoan);
        balance.text = BalanceValue.ToString("F");
        LimitPay.text = limit_pay.ToString("F");
    }
    private void FixedUpdate()
    {
        int[] date = time_system.getDateTime();
        LoanPlayerController player = manager.playerStatus.loanPlayerController;

        int len = player.timer.Length;
        if (date[2] < player.timer[2] || player.timer[2] == 0) {SpendBtn.interactable = false; return; };
        if (date[1] < player.timer[1] || player.timer[1] == 0) {SpendBtn.interactable = false; return; };
        if (date[0] < player.timer[0] || player.timer[0] == 0) {SpendBtn.interactable = false; return; };

        if (SpendLoanAmount > totalLoan || SpendLoanAmount < limit_pay)
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
        int[] present_date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
        LoanPlayerController player = manager.playerStatus.loanPlayerController;
        float newDebt = player.GetDept() - leftLoan;
        float newInterest = player.GetloanInterest() - leftInterest;
        player.SetDebt(newDebt);
        player.SetloanInterest(newInterest);

        float newDeptStatus = manager.playerStatus.financial_detail.debt - SpendLoanAmount;
        manager.playerStatus.financial_detail.debt = newDeptStatus;

        float newValue = manager.playerStatus.player_accounts.getPocket()[section.ToString()] - SpendLoanAmount;
        manager.playerStatus.player_accounts.setPocket(section.ToString(), newValue);

        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "�����Թ���", account_type = "DE", income = 0, expense = SpendLoanAmount, currencyIncome_Type = section, currencyExpense_Type = section };
        bank_manager.player_status.addAccountsDetails(newAccountDetail);
        // bank
        float newAmount = bank_manager.bank_status.companyData.pocketCompany.getPocket()[section.ToString()] + SpendLoanAmount;
        bank_manager.bank_status.companyData.pocketCompany.setPocket(section.ToString(), newAmount);
        AccountsDetail newBankAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "�Թ���", account_type = "LI", income = SpendLoanAmount, expense = 0, currencyIncome_Type = section, currencyExpense_Type = section };
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

        UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
        mail_manager.AddMail("Dept", $"�س����˹���Թ����",
            $"���ʴ������Թ�� �س����˹������ ��͹��� �繨ӹǹ {SpendLoanAmount} <sprite index={(int)section}>", null, null);

        player.round = 0;
        resetData();
    }

    void resetData()
    {
        SpendLoanAmount = 0;
        BalanceValue = 0;
        InputSpendLoan.gameObject.GetComponent<TMP_InputField>().text = "0";
    }
}
