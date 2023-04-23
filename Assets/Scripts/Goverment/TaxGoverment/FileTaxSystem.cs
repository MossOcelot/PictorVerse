using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FileTaxSystem : MonoBehaviour
{
    public float InputRI;
    public float InputFI;
    public float InputMI;

    public float InputEndownment;
    public float InputHeathInsurance;

    public float expensesRI;
    public float expensesFI;
    public float expensesMI;

    public float deduction;

    public float IncomeCalTax;
    public float netTax;
    public int late_day;

    [SerializeField]
    private bool CheckInfo;
    public bool FirstChangeT1;
    public bool FirstChangeT2;
    public bool FirstChangeT3;

    public bool isClick1;
    public bool isClick2;
    private PlayerStatus player;
    private InsuranceController insurance;

    public TextMeshProUGUI alert_TaxP3;

    public GameObject OutMoney;
    public GameObject InMoney;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        insurance = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
    }

    public void CalculateTaxAll()
    {
        CheckLateDay();
        CalculateExpense();
        CalculateDeduction();
        CalculateIncomeCalTax();
        CalNetTax();
    }
    public float AllIncome()
    {
        return InputRI + InputFI + InputMI;
    }

    public float AllExpense()
    {
        return expensesRI + expensesFI + expensesMI;
    }

    public void CalculateExpense()
    {
        expensesFI = Mathf.RoundToInt(InputFI * 0.5f);
        expensesRI = 0f;
        expensesMI = Mathf.RoundToInt(InputMI * 0.6f);
    }

    public void CheckLateDay()
    {
        GovermentPolicy goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();
        late_day = goverment.NonPayTaxDay;
    }

    public void CalculateDeduction()
    {
        GovermentPolicy goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();

        if(InputEndownment > goverment.govermentPolicy.maxdeductionEndowment)
        {
            deduction += goverment.govermentPolicy.maxdeductionEndowment;
        } else
        {
            deduction += InputEndownment;
        }

        if(InputHeathInsurance > goverment.govermentPolicy.maxdeductionHealthInsurance)
        {
            deduction += goverment.govermentPolicy.maxdeductionHealthInsurance;
        } else
        {
            deduction += InputHeathInsurance;
        }
    }

    public void CalculateIncomeCalTax()
    {
        float allIncome = AllIncome();
        float allExpense = AllExpense();
        IncomeCalTax = allIncome - allExpense - deduction;
    }

    public void CalNetTax()
    {

       netTax = player.PayTaxes(IncomeCalTax);
       if(netTax == 0)
       {
            deduction = 0;
       }
    }

    public bool GetCheck()
    {
        return CheckInfo;
    }

    [NonSerialized]
    public bool CheckMI;
    [NonSerialized]
    public bool CheckRI;
    [NonSerialized]
    public bool CheckFI;
    [NonSerialized]
    public bool CheckIncome;
    [NonSerialized]
    public bool CheckEndowment; 
    [NonSerialized]
    public bool CheckHealthInsurance; 
    [NonSerialized]
    public bool CheckDeduction;

    public void ClearCheck()
    {
        CheckInfo = false;
    }
    public void CheckInformation()
    {
        CheckMI = player.GetIncomeTypeInYear("MI") == InputMI;
        CheckRI = player.GetIncomeTypeInYear("RI") == InputRI;
        CheckFI = player.GetIncomeTypeInYear("FI") == InputFI;

       
        CheckIncome = CheckMI && CheckRI && CheckFI;

        CheckEndowment = insurance.GetInsuranceCostInYear("Endowment") >= InputHeathInsurance;
        CheckHealthInsurance = insurance.GetInsuranceCostInYear("Health_insurance") >= InputEndownment;
        Debug.Log($"MI: {player.GetIncomeTypeInYear("MI")} RI: {player.GetIncomeTypeInYear("RI")} E: {insurance.GetInsuranceCostInYear("Endowment")} H:{insurance.GetInsuranceCostInYear("Health_insurance")}");
        CheckDeduction = CheckEndowment && CheckHealthInsurance;

        if (CheckIncome && CheckDeduction)
        {
            CheckInfo = true;
            if(netTax == 0)
            {
                alert_TaxP3.text = $"คุณไม่ถึงเกณฑ์เสียภาษี";
                alert_TaxP3.color = Color.green;

                StartCoroutine(CountdownDestroy());
            }
        }
        else
        {
            CheckInfo = false;
        }
    }

    private IEnumerator CountdownDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void CheckPocket()
    {
        string section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
        float myCash = player.player_accounts.getPocket()[section];

        if(myCash >= netTax)
        {
            InMoney.SetActive(true);
            InMoney.gameObject.GetComponent<UIInMoney>().SetData(myCash, netTax);
            OutMoney.SetActive(false);
        }
        else
        {
            InMoney.SetActive(false);
            OutMoney.SetActive(true);
            OutMoney.gameObject.GetComponent<UIInMoney>().SetData(myCash, netTax);
        }
    }

    public void PayTax()
    {
        GovermentPolicy goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();

        Timesystem date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        int[] dateTime = date.getDateTime();

        SceneStatus.section section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
        float newValue = player.player_accounts.getPocket()[section.ToString()] - netTax;

        player.player_accounts.setPocket(section.ToString(), newValue);

        float newCost = goverment.govermentStatus.goverment.govermentPockets.getPocket()[section.ToString()] + netTax;

        goverment.govermentStatus.goverment.govermentPockets.setPocket(section.ToString(), newCost);

        AccountsDetail account_Player = new AccountsDetail() { date = dateTime, accounts_name = "จ่ายภาษี", account_type = "TAX", income = 0, expense = netTax, currencyIncome_Type = section, currencyExpense_Type = section };
        player.addAccountsDetails(account_Player);

        AccountsDetail account_goverment = new AccountsDetail() { date = dateTime, accounts_name = "เก็บภาษี", account_type = "PayTax", income = netTax, expense = 0, currencyIncome_Type = section, currencyExpense_Type = section };
        goverment.govermentStatus.addAccountsDetail(account_goverment);

        UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
        mail_manager.AddMail("Goverment", $"จ่ายภาษีแล้ว ของปี {dateTime[2] - 1}",
            $"ขอแสดงความยินดี คุณจ่ายภาษีแล้ว เป็นจำนวน {netTax} <sprite index={(int)section}>", null, null);

        // reset
        goverment.NonPayTaxDay = 0;
        goverment.GovermentAlert = false;

        Destroy(gameObject);
    }
}
