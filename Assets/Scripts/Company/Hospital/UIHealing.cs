using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class UIHealing : MonoBehaviour
{
    public TextMeshProUGUI disease_name;
    public TextMeshProUGUI disease_cost;
    public TextMeshProUGUI my_money;
    public TextMeshProUGUI total_cost;
    public TextMeshProUGUI insurance_name;
    public TextMeshProUGUI insurance_description;
    public TextMeshProUGUI insurance_protect;
    public TextMeshProUGUI total_insurance_cost;
    public Image logo;

    private PlayerStatus playerStatus;
    private InsuranceController insurance_controller;

    float player_cost;
    float cost_no_insurance;
    float player_pocket;
    SceneStatus.section section;

    public GameObject InsuranceDetails;
    public GameObject InsuranceDescription;
    public GameObject InsuranceBtn;
    public GameObject LoanBtn;
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        insurance_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
        player_pocket = playerStatus.player_accounts.getPocket()[section.ToString()];   
    }

    public void SetData(string name, float cost)
    {
        
        player_cost = cost;

        disease_name.text = $"โรค : {name}";
        disease_cost.text = $"ค่ารักษา : {cost}";

        my_money.text = $"ยอดเงินปัจจุบัน : {player_pocket} <sprite index={(int)section}>";
        total_cost.text = $"{cost} <sprite index={(int)section}>";
        cost_no_insurance = cost;

        if (insurance_controller.GetPlayer_hearth_insurance().insurance == null)
        {
            insurance_name.text = "ไม่มี";
            InsuranceDetails.SetActive(false);
            InsuranceDescription.SetActive(true);
            InsuranceBtn.SetActive(false);
            LoanBtn.SetActive(true);
            return;
        }
        logo.sprite = insurance_controller.GetPlayer_hearth_insurance().insurance.logo;
        float protect_cost = insurance_controller.hearth_insurance_claim(cost, section.ToString(), "Get");
        InsuranceItems insurance = insurance_controller.GetPlayer_hearth_insurance();
        string description = "คุ้มครองราคา " + insurance.insurance.insurance_percent + " % มูลค่าไม่เกิน " + insurance.insurance.insurance_limit.ToString("F") + " <sprite index=0>";

        insurance_name.text = insurance_controller.GetPlayer_hearth_insurance().insurance.insurance_name;
        insurance_description.text = description;
        insurance_protect.text = $"{protect_cost} <sprite index={(int)section}>";
        total_insurance_cost.text = $"{cost - protect_cost} <sprite index={(int)section}>";
    }

    public void SpendCost()
    {
        float newValue = player_pocket - player_cost;
        playerStatus.player_accounts.setPocket(section.ToString(), newValue );

        Timesystem date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        int[] dateTime = date.getDateTime();
        AccountsDetail account = new AccountsDetail() { date = dateTime, accounts_name = "ค่ารักษาพยาบาล", account_type = "TF", income = 0, expense = player_cost, currencyIncome_Type = SceneStatus.section.section1, currencyExpense_Type = SceneStatus.section.section1 };
        playerStatus.addAccountsDetails(account);
        ClearDesease();
    }

    public void SpendInsuranceCost()
    {
        SpendCost();
        insurance_controller.hearth_insurance_claim(player_cost, section.ToString(), "claim");
    }

    public void SpendByLoan()
    {
        int[] present_date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();

        LoanPlayerController player = playerStatus.loanPlayerController;
        float newDebt = player.GetDept() + player_cost;
        player.SetDebt(newDebt);

        float newDeptStatus = playerStatus.financial_detail.debt + player_cost;
        playerStatus.financial_detail.debt = newDeptStatus;

        float newValue = playerStatus.player_accounts.getPocket()[section.ToString()] + player_cost;
        playerStatus.player_accounts.setPocket(section.ToString(), newValue);

        AccountsDetail newAccountDetail = new AccountsDetail() { date = present_date, accounts_name = "กู้เงิน", account_type = "TF", income = player_cost, expense = 0, currencyIncome_Type = SceneStatus.section.section1, currencyExpense_Type = SceneStatus.section.section1 };
        playerStatus.addAccountsDetails(newAccountDetail);

        AccountsDetail account = new AccountsDetail() { date = present_date, accounts_name = "ค่ารักษาพยาบาล", account_type = "buy", income = 0, expense = player_cost, currencyIncome_Type = SceneStatus.section.section1, currencyExpense_Type = SceneStatus.section.section1 };
        playerStatus.addAccountsDetails(account);
        ClearDesease();
    }
    private void ClearDesease()
    {
        StatusEffectController statusEffect = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<StatusEffectController>();
        statusEffect.ClearEffects();
    }
}
