using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FinancialAnalytic : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player_status;
    [SerializeField]
    private InsuranceController player_insurance;
    [SerializeField]
    private CareerPlayer player_career;
    [SerializeField]
    private LoanPlayerController player_loan;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player_status = player.GetComponent<PlayerStatus>();
        player_insurance = player.GetComponent<InsuranceController>();
        player_career = player.GetComponent<CareerPlayer>();
        player_loan = player.GetComponent<LoanPlayerController>();
    }

    private List<IncomeExpenseData> CleanIncomeAndExpense()
    {
        List<AccountsDetail> accounts = player_status.getAccountsDetails();
        //accounts = accounts.Last();

        int present_month = 0;
        int present_year = 0;

        List<IncomeExpenseData> incomeExpense = new List<IncomeExpenseData>();
        IncomeExpenseData data = new IncomeExpenseData();
        foreach (AccountsDetail account in accounts)
        {
            int[] date = account.date;
            string type = account.account_type;
            float income = account.income;
            float expense = account.expense;

            if (date[1] == present_month && date[2] == present_year)
            {
                if (income > 0 && expense > 0)
                {
                    AccountAmount income_amount = new AccountAmount(AccountAmount.AccountType.EX, income);
                    data.income.Add(income_amount);

                    AccountAmount expense_amount = new AccountAmount(AccountAmount.AccountType.EX, expense);
                    data.expense.Add(expense_amount);
                }
                else if (income > 0)
                {
                   // AccountAmount income_amount = GetIncomeAccountAmount(type, income);
                   // data.income.Add(income_amount);
                }
                else if (expense > 0)
                {
                  //  AccountAmount expense_amount = GetExpenseAccountAmount(type, expense);
                  //  data.expense.Add(expense_amount);
                }
            }
            else
            {
                if(present_month != 0 || present_year != 0)
                {
                    incomeExpense.Add(data);
                    data = new IncomeExpenseData();
                    data.date = date;
                }
                
                present_month = date[1];
                present_year = date[2];
            }
        }
        return incomeExpense;
    }
}

[System.Serializable]
public class IncomeExpenseData
{
    public int[] date;
    public List<AccountAmount> income;
    public List<AccountAmount> expense;
}

[System.Serializable]
public class AccountAmount
{
    public enum AccountType { FI, RI, MI, LI, TE, EE, TF, IEE, IHE, TAX, DE, OE, SE, EX }
    public AccountType type;
    public float amount;

    public AccountAmount (AccountType type, float amount)
    {
        this.type = type;
        this.amount = amount;
    }
}
