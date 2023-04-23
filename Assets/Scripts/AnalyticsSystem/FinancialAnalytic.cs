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
    [SerializeField]
    private AccountBankOperation account_bank_operation;

    [SerializeField]
    private Timesystem time_system;

    [SerializeField]
    private List<IncomeExpenseData> incomeExpenseData;


    public UIShowAnalytic show_analytic;
    public CareerSO[] careerData;

    public bool isRecommendTomonth;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player_status = player.GetComponent<PlayerStatus>();
        player_insurance = player.GetComponent<InsuranceController>();
        player_career = player.GetComponent<CareerPlayer>();
        player_loan = player.GetComponent<LoanPlayerController>();
        account_bank_operation = GameObject.FindGameObjectWithTag("AccountOperation").gameObject.GetComponent<AccountBankOperation>();
        time_system = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>(); 
    }

    private void Update()
    {
        int day = time_system.getDateTime()[0];
        if(day == 12 && !isRecommendTomonth)
        {
            int count = player_status.getAccountsDetails().Count;

            isRecommendTomonth = true;
            if (count <= 0) return;
            incomeExpenseData = CleanIncomeAndExpense();
            FinancialAnalyticPlayer(incomeExpenseData);
            GameObject.FindGameObjectWithTag("AccountDetailSystem").gameObject.GetComponent<DescriptionAlertController>().alert_description = show_analytic.alert_details;
        } 
        

        if(day != 12)
        {
            isRecommendTomonth = false;
        }
        
        
    }

    public void FinancialAnalyticPlayer(List<IncomeExpenseData> incomeExpenseData)
    {
        // Check Cash flow
        int predictedMonth = PredictedMonths(incomeExpenseData);

        if(predictedMonth == 0)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.angry, AlertAnalytic.Alert_type.cashflow,
                $"��͹��� Cash Flow �ͧ�س�Դź ��Ң��й������͹�Ѵ令س����������������� ����Ŵ��¨���");
        }
        else if(predictedMonth <= 3)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.angry, AlertAnalytic.Alert_type.cashflow,
                $"�س���͡�� Cash Flow ���͡�����Թʴ �еԴź��ա {predictedMonth} ��͹ ���繵�ͧ����������� ���� �����Ѵ����Ŵ��������ŧ" +
                $"�� Cash Flow ��� ������Թʴ������Ѻ�� ��Ш����͡� �� Cash Flow ����õԴź������¤������ ������Թ���(����Ѻ) ����ҡ���ҡ�����Թ�͡(��¨���)");
        }
        else if(predictedMonth <= 6) 
        {
            // Alert Green
            show_analytic.Add_Alert_Details(1, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.cashflow,
                $"�س���͡�� Cash Flow ���͡�����Թʴ �еԴź��ա {predictedMonth} ��͹ ���繵�ͧ����������� ���� �����Ѵ����Ŵ��������ŧ" +
                $"�� Cash Flow ��� ������Թʴ������Ѻ�� ��Ш����͡� �� Cash Flow ����õԴź������¤������ ������Թ���(����Ѻ) ����ҡ���ҡ�����Թ�͡(��¨���)");
        } 
        else if(predictedMonth <= 12)
        {
            // good
            show_analytic.Add_Alert_Details(1, AlertAnalytic.NPC_RECOMMEND_TYPE.admire, AlertAnalytic.Alert_type.cashflow,
               $"�������ҡ�ҡʶԵ��ʹյ�س���ա�����Թʴ��ա {predictedMonth} ��͹ ���������дѺ���� �ô�ѡ�һ���ҳ Cash Flow Ẻ������" +
               $"�� Cash Flow ��� ������Թʴ������Ѻ�� ��Ш����͡� �� Cash Flow ����õԴź������¤������ ������Թ���(����Ѻ) ����ҡ���ҡ�����Թ�͡(��¨���)");
        }
        else if (predictedMonth <= 24)
        {
            // very good
            show_analytic.Add_Alert_Details(2, AlertAnalytic.NPC_RECOMMEND_TYPE.admire, AlertAnalytic.Alert_type.cashflow,
               $"�������ҡ � �ҡʶԵ��ʹյ�س���ա�����Թʴ��ա {predictedMonth} ��͹ ���������дѺ���� �ô�ѡ�һ���ҳ Cash Flow Ẻ������" +
               $"�� Cash Flow ��� ������Թʴ������Ѻ�� ��Ш����͡� �� Cash Flow ����õԴź������¤������ ������Թ���(����Ѻ) ����ҡ���ҡ�����Թ�͡(��¨���)");
        }
        else if (predictedMonth <= 36)
        {
            // very very good
            show_analytic.Add_Alert_Details(3, AlertAnalytic.NPC_RECOMMEND_TYPE.admire, AlertAnalytic.Alert_type.cashflow,
              $"�������ҡ ��� �ҡʶԵ��ʹյ�س���ա�����Թʴ��ա {predictedMonth} ��͹ ���������дѺ���� �ô�ѡ�һ���ҳ Cash Flow Ẻ������" +
              $"�� Cash Flow ��� ������Թʴ������Ѻ�� ��Ш����͡� �� Cash Flow ����õԴź������¤������ ������Թ���(����Ѻ) ����ҡ���ҡ�����Թ�͡(��¨���)");
        }

        IncomeExpenseData data = incomeExpenseData[incomeExpenseData.Count - 1];
        float AllIncome = data.All_income();
        float AllExpense = data.All_expense();

        // Check Saving For month
        bool status_saving = CheckSavingRateStatus(AllIncome);
        if(status_saving)
        {
            // pass
            show_analytic.Add_Alert_Details(2, AlertAnalytic.NPC_RECOMMEND_TYPE.admire, AlertAnalytic.Alert_type.saving,
               $"��ҹ��͹���س�����Թ�ҡ���� 20% �ͧ�����" +
               $"���������͹ ��Ҥ�����Թ�����¡��� 20% �ͧ����� ����� 80/20 ��觡������Թ �Ъ������������Թ���������� ����Թ�������� �Թ��Ѿ����ع���¹ ����ҡ�������ջѭ�� ������ö���͡������ѹ��" +
               $"�������س�������ö���Ѻ��� Credit ��ҡ��ýҡ�Թ㹸�Ҥ��");
        }
        else
        {
            // wrong
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.saving,
               $"��ҹ��͹���س�����Թ���¡��� 20% �ͧ����� �س������Թ�ҡ���ҹ��" +
               $"���������͹ ��Ҥ�����Թ�����¡��� 20% �ͧ����� ����� 80/20 ��觡������Թ �Ъ������������Թ���������� ����Թ�������� �Թ��Ѿ����ع���¹ ����ҡ�������ջѭ�� ������ö���͡������ѹ��" +
               $"�������س�������ö���Ѻ��� Credit ��ҡ��ýҡ�Թ㹸�Ҥ��");
        }

        // Check Income < Expense 
        bool status_income_expense = CheckStatusIncomeExpense(AllIncome, AllExpense);
        if (!status_income_expense)
        {
            // recommend
            CareerSO career = RecommendCareer(AllIncome - AllExpense);
            if(career == null)
            {
                // recommend new gameplay
                show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.angry, AlertAnalytic.Alert_type.career,
               $"�س�ջѭ�ҷҧ����Թ ����Ҫվ��Ш��������Ѩ�غѹ������Ҫվ �˹�������Թ��§�͡Ѻ��¨��·��س�� " +
               $"����Ң��й����سŴ�������� ��Ф��� � ��ش���ҧ˹���ҡ�������ö��ش��ѹ�� ��о��������Թ �ҡ������� � ��������¤س������ҡ���� 1 �Ҫվ ���Ҩ���Ҫվ����� �蹡��仢ش��� ��ҿ�ͧ仢��" +
               $" �繵� �������س��������������");

                RecommendSuggestExpense(incomeExpenseData);
                RecommendSuggestIncome(incomeExpenseData);
            }
            else
            {
                // recommend new Career
                show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.career,
               $"�س�ջѭ�ҷҧ����Թ ��Ң��й��Ҫվ {career.career_name} ���������Թ��͹ {career.salary_default} ����ҡ�͡Ѻ��¨��¢ͧ�س " +
               $"�¤س���Ŵ��¨��� ����������ҧ����������仴���");

                RecommendSuggestExpense(incomeExpenseData);
                RecommendSuggestIncome(incomeExpenseData);
            }
        }

    }

    private void RecommendSuggestExpense(List<IncomeExpenseData> incomeExpenseData)
    {
        List<ExpenseTypeRate> expenseTypeRates = CheckExpenseTypeRate(incomeExpenseData);
        ExpenseTypeRate.MaxExpenseType maxExpenseType = expenseTypeRates[expenseTypeRates.Count - 1].CheckMaxExpenseType();
        AccountAmount.AccountType type = maxExpenseType.accountType;
        if (type == AccountAmount.AccountType.DE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
               $"�س����¨��·�����仡Ѻ˹���Թ�������͹���ѵ����ǹ����ҡ����ش ��Ң��й����س��ش��˹������ ��Ъ���˹�����������¡�͹ ������觶�Ҥس��˹������ ����觷����͡���·���ͧ�����������͹����ҡ ");
        }
        else if (type == AccountAmount.AccountType.IEE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س����¨��·�����仡Ѻ��Сѹ���Ե�ҡ� ���͹��� ��Ң��й����س���ͻ�Сѹ�����������Ѻ����ͧ�������ҡ�Թ�");
        }
        else if (type == AccountAmount.AccountType.IHE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س����¨��·�����仡Ѻ��Сѹ�آ�Ҿ�ҡ� ���͹��� ��Ң��й����س���ͻ�Сѹ�����������Ѻ����ͧ�������ҡ�Թ�");
        }
        else if (type == AccountAmount.AccountType.TF)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س����¨��·�����仡Ѻ����ѡ�� �ҡ�Թ���Ң��й����س���ͻ�Сѹ�آ�Ҿ ���ͪ���Ŵ�����������ǹ�ç���");
        }
        else if (type == AccountAmount.AccountType.SE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س���Թ�����ҡ���͹��� ��Ң��й����س�纻���ҳ 20% ���ͨ��ҡ���ҹ����� ��������Թ���ѧ�ͧ����ͧ�Թ�");
        }
        else if (type == AccountAmount.AccountType.TE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س�դ�����������Ѻ����Թ�ҧ �����Թ� ��Ң��й����س�ҧἹ������Թ�ҧ价���ҧ � ���ҧ�ͺ�ͺ");
        }
        else if (type == AccountAmount.AccountType.EE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س�դ�����������Ѻ�Թ�ҡ�Թ� �س��ë�������÷��ʹ�����ҡ���Թ�");
        }
        else if (type == AccountAmount.AccountType.OE)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.expense,
              $"�س�դ�����������Ѻ�����Թ������ � �ҡ�Թ� �ҡ�Թ��� ������仹�����������Դ����� ��Ң��й����سŴ�����������ǹ�ç���");
        }
    }

    private void RecommendSuggestIncome(List<IncomeExpenseData> incomeExpenseData)
    {
        List<IncomeTypeRate> incomeTypeRates = CheckIncomeTypeRate(incomeExpenseData);
        IncomeTypeRate.MaxIncomeType maxIncomeType = incomeTypeRates[incomeTypeRates.Count - 1].CheckMaxIncomeType();
        AccountAmount.AccountType type = maxIncomeType.accountType;
        if (type == AccountAmount.AccountType.FI)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.income,
              $"�س�������ҡ��÷ӧҹ�Ҫվ����� ��ҧ � �ҡ����ش ��Ң��й����س�ӧҹ�����ҡ��������������������ᵡ��ҧ�ѹ " +
              $"���ͻ�ͧ�ѹ��������§�ҡ�س�������ö�������ҡ��ǹ��� ���ͧ�ҡ�Ҫվ����й���������դ�����蹤� �ҧ�����");
        }
        else if (type == AccountAmount.AccountType.RI)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.income,
              $"�س�������ҡ��÷ӧҹ�Ҫվ��Ш� ��ҧ � �ҡ����ش ��Ң��й����س�ӧҹ�����ҡ��������������������ᵡ��ҧ�ѹ " +
              $"���Фس�Ҩ�ж١����͡�� ��Ң��й����س���Ҫվ�����㹪�ǧ��ҧ � ��ѧ��ԡ�ҹ");
        }
        else if (type == AccountAmount.AccountType.MI)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.income,
              $"�س�������ҡ��÷ӧҹ�Ҫվ��Ң�� ��ҧ � �ҡ����ش ��Ң��й����س�ӧҹ�����ҡ��������������������ᵡ��ҧ�ѹ ");
        }
        else if (type == AccountAmount.AccountType.LI)
        {
            show_analytic.Add_Alert_Details(0, AlertAnalytic.NPC_RECOMMEND_TYPE.recommend, AlertAnalytic.Alert_type.income,
              $"��͹���������Ѻ�ҡ��á���Թ�ҡ����ش �س������Թ����������ҧ�١��ͧ��С������Դ����� �����Ң��й����س���Ҫվ�������ҧ � ��������ö���Թ�Ҫ���˹����س������� ");
        }
    }

    private bool CheckSavingRateStatus(float all_income)
    {
        // true = pass , false = wrong
        List<AccountData> account_data = account_bank_operation.GetAccountData();

        string accountID = player_status.GetAccountID();
        float total = 0;
        foreach (AccountData account_d in account_data)
        {
            foreach (AccountData.Accounts account in account_d.accountsDB)
            {
                if (account.account_id == accountID)
                {
                    List<AccountList> account_list = account.account_list;

                    int present_month = time_system.getDateTime()[1];

                    foreach(AccountList list in account_list)
                    {
                        if(present_month == list.date[1])
                        {
                            float deposit = list.deposit;
                            float withdrawal = list.withdrawal;
                            
                            float balance = deposit - withdrawal;
                            total += balance;
                        }else
                        {
                            break;
                        }
                    }

                }
            }
        }
        float percent = (total / all_income) * 100;
        if(percent >= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckStatusIncomeExpense(float AllIncome, float AllExpense)
    {
        if(AllExpense > AllIncome)
        {
            return false;
        }
        return true;
    }

    private List<IncomeExpenseData> CleanIncomeAndExpense()
    {
        List<AccountsDetail> accounts = player_status.getAccountsDetails();
        int[] present_date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
        int present_month = present_date[1];
        int present_year = present_date[2];

        List<IncomeExpenseData> incomeExpense = new List<IncomeExpenseData>();
        IncomeExpenseData data = new IncomeExpenseData();

        int len = accounts.Count;
        for(int i = len - 1; i >= 0; i--)
        {
            AccountsDetail account = accounts[i];
            int[] date = account.date;
            string type = account.account_type;
            float income = account.income;
            float expense = account.expense;

            data.date = date;
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
                    AccountAmount income_amount = GetIncomeAccountAmount(type, income);
                    data.income.Add(income_amount);
                }
                else if (expense > 0)
                {
                    AccountAmount expense_amount = GetExpenseAccountAmount(type, expense);
                    data.expense.Add(expense_amount);
                }
            }
            else
            {
                incomeExpense.Add(data);
                data = new IncomeExpenseData();
                data.date = date;
                present_month = date[1];
                present_year = date[2];
            }

        }
        incomeExpense.Add(data);
        return incomeExpense;
    }

    private int PredictedMonths(List<IncomeExpenseData> incomeExpenseData) // Pass
    {
        List<float> incomes = new List<float>();
        List<float> expenses = new List<float>();


        int old_month = 0;
        int index = 0;
        foreach(IncomeExpenseData data in incomeExpenseData)
        {
            int[] date = data.date;
            if (date[1] != old_month)
            {
                incomes.Add(data.All_income());
                expenses.Add(data.All_expense());

                old_month = date[1];
            }
            index++;
        }

        float[] incomes_arr = incomes.ToArray();
        float[] expenses_arr = expenses.ToArray();

        float[] differences = new float[incomes_arr.Length];
        for (int i = 0; i < incomes_arr.Length; i++)
        {
            differences[i] = incomes_arr[i] - expenses_arr[i];
        }

        float[] incomeGrowthRates = new float[incomes_arr.Length];
        float[] expenseGrowthRates = new float[expenses_arr.Length];
        for (int i = 1; i < differences.Length; i++)
        {
            float incomeGrowthRate = (incomes_arr[i] - incomes_arr[i - 1]) / incomes_arr[i - 1] * 100;
            float expenseGrowthRate = (expenses_arr[i] - expenses_arr[i - 1]) / expenses_arr[i - 1] * 100;
            incomeGrowthRates[i] = incomeGrowthRate;
            expenseGrowthRates[i] = expenseGrowthRate;
        }

        float AllincomeGrowthRate = incomeGrowthRates.Sum() / incomeGrowthRates.Length;
        float AllexpenseGrowthRate = expenseGrowthRates.Sum() / expenseGrowthRates.Length;

        int predictedMonths = 0;
        float predictIncome = incomes_arr[incomes_arr.Length - 1];
        float predictExpense = expenses_arr[expenses_arr.Length - 1];
        while(true)
        {
            predictIncome *= (1 + (AllincomeGrowthRate / 100));
            predictExpense *= (1 + (AllexpenseGrowthRate / 100));

            float difference = predictIncome - predictExpense;
            if(difference < 0)
            {
                break;
            }
            if(predictedMonths > 36)
            {
                break;
            }
            predictedMonths++;
        }
        return predictedMonths;
    }

    private List<IncomeTypeRate> CheckIncomeTypeRate(List<IncomeExpenseData> incomeExpenseData)
    {
        List<IncomeTypeRate> incomeTypeRateList = new List<IncomeTypeRate>();
        foreach (IncomeExpenseData data in incomeExpenseData)
        {
            List<AccountAmount> income_list = data.income;
            Dictionary<AccountAmount.AccountType, float> amount_income = new Dictionary<AccountAmount.AccountType, float>()
            {
                { AccountAmount.AccountType.RI, 0 },
                { AccountAmount.AccountType.FI, 0 },
                { AccountAmount.AccountType.MI, 0 },
                { AccountAmount.AccountType.LI, 0 }

            };


            foreach (AccountAmount expense in income_list)
            {
                AccountAmount.AccountType type = expense.type;
                float amount = expense.amount;

                amount_income[type] += amount;
            }
            IncomeTypeRate incomeTypeRate = new IncomeTypeRate();
            incomeTypeRate.date = data.date;
            incomeTypeRate.amount_income = amount_income;
            incomeTypeRateList.Add(incomeTypeRate);
        }

        return incomeTypeRateList;
    }

    private List<ExpenseTypeRate> CheckExpenseTypeRate(List<IncomeExpenseData> incomeExpenseData)
    {
        List<ExpenseTypeRate> expenseTypeRateList = new List<ExpenseTypeRate>();
        foreach(IncomeExpenseData data in incomeExpenseData)
        {
            List<AccountAmount> expense_list = data.expense;
            Dictionary<AccountAmount.AccountType, float> amount_expenses = new Dictionary<AccountAmount.AccountType, float>()
            {
                { AccountAmount.AccountType.TE, 0 },
                { AccountAmount.AccountType.EE, 0 },
                { AccountAmount.AccountType.TF, 0 },
                { AccountAmount.AccountType.IEE, 0 },
                { AccountAmount.AccountType.IHE, 0 },
                { AccountAmount.AccountType.TAX, 0 },
                { AccountAmount.AccountType.DE, 0 },
                { AccountAmount.AccountType.OE, 0 },
                { AccountAmount.AccountType.SE, 0 },
                { AccountAmount.AccountType.EX, 0 },
            
            };
            

            foreach(AccountAmount expense in expense_list)
            {
                AccountAmount.AccountType type = expense.type;
                float amount = expense.amount;

                amount_expenses[type] += amount;
            }
            ExpenseTypeRate expenseTypeRate = new ExpenseTypeRate();
            expenseTypeRate.date = data.date;
            expenseTypeRate.amount_expenses = amount_expenses;
            expenseTypeRateList.Add(expenseTypeRate);
        }

        return expenseTypeRateList;
    }
     
    private AccountAmount GetIncomeAccountAmount(string type, float amount)
    {
        AccountAmount account = null;
        if (type == "FI")
        {
            account = new AccountAmount(AccountAmount.AccountType.FI, amount);
        }
        else if (type == "RI")
        {
            account = new AccountAmount(AccountAmount.AccountType.RI, amount);
        }
        else if (type == "MI") 
        {
            account = new AccountAmount(AccountAmount.AccountType.MI, amount);
        }
        else if (type == "LI")
        {
            account = new AccountAmount(AccountAmount.AccountType.LI, amount);
        }
        Debug.Log("Test1.1");
        return account;
    }

    private AccountAmount GetExpenseAccountAmount(string type, float amount)
    {
        AccountAmount account = null;
        if(type == "TE")
        {
            account = new AccountAmount(AccountAmount.AccountType.TE, amount);
        }
        else if (type == "EE")
        {
            account = new AccountAmount(AccountAmount.AccountType.EE, amount);
        }
        else if (type == "TF")
        {
            account = new AccountAmount(AccountAmount.AccountType.TF, amount);
        }
        else if (type == "IEE")
        {
            account = new AccountAmount(AccountAmount.AccountType.IEE, amount);
        }
        else if (type == "IHE")
        {
            account = new AccountAmount(AccountAmount.AccountType.IHE, amount);
        }
        else if (type == "TAX")
        {
            account = new AccountAmount(AccountAmount.AccountType.TAX, amount);
        }
        else if (type == "DE")
        {
            account = new AccountAmount(AccountAmount.AccountType.DE, amount);
        }
        else if (type == "OE")
        {
            account = new AccountAmount(AccountAmount.AccountType.OE, amount);
        }
        else if (type == "SE")
        {
            account = new AccountAmount(AccountAmount.AccountType.SE, amount);
        }

        return account;
    }

    private CareerSO RecommendCareer(float shortage_amount) // input income - expense abs
    {
        CareerSO career = player_career.Career;
        if(career == null)
        {
            float salary = 0;
            foreach(CareerSO other_career in careerData)
            {
                if(other_career.salary_default > salary)
                {
                    float differense_salar = other_career.salary_default - salary;
                    if(differense_salar > shortage_amount)
                    {
                        return other_career;
                    }
                }
            }
            return null;
        }
        else
        {
            float salary = career.salary_default;
            foreach (CareerSO other_career in careerData)
            {
                if (other_career.salary_default > salary)
                {
                    float differense_salar = other_career.salary_default - salary;
                    if (differense_salar > shortage_amount)
                    {
                        return other_career;
                    }
                }
            }
            return null;
        }
    }
}

[System.Serializable]
public class IncomeExpenseData
{
    public int[] date;
    public List<AccountAmount> income = new List<AccountAmount>();
    public List<AccountAmount> expense = new List<AccountAmount>();

    public float All_income()
    {
        float allIncome = 0;
        foreach(AccountAmount account in income)
        {
            allIncome += account.amount;
        }
        return allIncome;
    }

    public float All_expense()
    {
        float allExpense = 0;
        foreach(AccountAmount account in expense)
        {
            allExpense += account.amount;
        }
        return allExpense;
    }
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

public class IncomeTypeRate
{
    public int[] date;
    public Dictionary<AccountAmount.AccountType, float> amount_income;

    public float CheckPercentRate(AccountAmount.AccountType type)
    {
        float all_income = 0;
        float amount = amount_income[type];
        foreach (float value in amount_income.Values)
        {
            all_income += value;
        }
        if (all_income == 0) return 0;

        return (amount / all_income) * 100;
    }

    [System.Serializable]
    public class MaxIncomeType
    {
        public AccountAmount.AccountType accountType;
        public float amount_per;

        public MaxIncomeType(AccountAmount.AccountType type, float new_per)
        {
            accountType = type;
            amount_per = new_per;
        }
    }
    public MaxIncomeType CheckMaxIncomeType()
    {
        float max_income_per = 0;
        AccountAmount.AccountType max_income_type = AccountAmount.AccountType.EX;
        foreach (AccountAmount.AccountType type in amount_income.Keys)
        {
            float per = CheckPercentRate(type);
            if (max_income_per < per)
            {
                max_income_per = per;
                max_income_type = type;
            }
        }

        return new MaxIncomeType(max_income_type, max_income_per);
    }
}

public class ExpenseTypeRate
{
    public int[] date;
    public Dictionary<AccountAmount.AccountType, float> amount_expenses;

    public float CheckPercentRate(AccountAmount.AccountType type)
    {
        float all_expense = 0;
        float amount = amount_expenses[type];
        foreach(float value in amount_expenses.Values)
        {
             all_expense += value;
        }
        if (all_expense == 0) return 0;

        return (amount / all_expense) * 100;
    }

    [System.Serializable]
    public class MaxExpenseType
    {
        public AccountAmount.AccountType accountType;
        public float amount_per;

        public MaxExpenseType(AccountAmount.AccountType type, float new_per)
        {
            accountType = type;
            amount_per = new_per;
        }
    }
    public MaxExpenseType CheckMaxExpenseType()
    {
        float max_expense_per = 0;
        AccountAmount.AccountType max_expense_type = AccountAmount.AccountType.EX;
        foreach(AccountAmount.AccountType type in amount_expenses.Keys)
        {
            float per = CheckPercentRate(type);
            if(max_expense_per < per)
            {
                max_expense_per = per;
                max_expense_type = type;
            }
        }

        return new MaxExpenseType(max_expense_type, max_expense_per);
    }
}
