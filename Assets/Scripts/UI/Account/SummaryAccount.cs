using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SummaryAccount : MonoBehaviour
{
    public PlayerStatus player_status;
    public int section;
    public string section_name;

    [SerializeField]
    private float totalAssets;
    [SerializeField]
    private float totalDebtAssets;
    [SerializeField]
    private float income;
    [SerializeField]
    private float expense;
    [SerializeField]
    private float total;

    [SerializeField]
    private PieChart pie_chart;

    [SerializeField]
    private TextMeshProUGUI asset_text;
    [SerializeField]
    private TextMeshProUGUI debt_text;
    [SerializeField]
    private TextMeshProUGUI income_text;
    [SerializeField]
    private TextMeshProUGUI expense_text;
    [SerializeField]
    private TextMeshProUGUI total_text;
    // Start is called before the first frame update
    void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        section_name = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        section = (int)GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }

    // Update is called once per frame
    void Update()
    {
        totalAssets = 0;
        totalDebtAssets = 0;
        income = 0;
        expense = 0;
        total = 0;

        GetTotalAssets();
        GetTotalAccountDetail();
        totalAssets += player_status.GetFinancial_balance();
        totalDebtAssets += player_status.GetFinancial_debt();
        total = totalAssets - totalDebtAssets + income - expense;

        asset_text.text = totalAssets.ToString("F") + " $";
        debt_text.text = totalDebtAssets.ToString("F") + " $";
        income_text.text = income.ToString("F") + " $";
        expense_text.text = expense.ToString("F") + " $";
        total_text.text = total.ToString("F") + " $";

        // update pie_chart
        pie_chart.AddValues(new float[] { income, expense, totalAssets, totalDebtAssets });
    }

    private void GetTotalAssets()
    {
        Dictionary<string, float> player_account = player_status.player_accounts.getPocket();

        int n = 0;
        foreach(string key in player_account.Keys)
        {
            if (key == section_name)
            {
                totalAssets += player_account[key];
            } else
            {
                totalAssets += player_account[key] * new ExchangeRate().getExchangeRate(n, section);
            }
            n++;
        }
    }

    private void GetTotalAccountDetail()
    {
        List<AccountsDetail> player_account_details = player_status.getAccountsDetails();

        foreach(AccountsDetail account in player_account_details)
        {
            income += account.income;
            expense += account.expense;
        }
    }
    
}
