using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class UIShowAnalytic : MonoBehaviour
{
    public List<AlertAnalytic> alert_details;

}

[System.Serializable]
public class AlertAnalytic
{
    public int importance_level;
    public bool recommend_career;
    public bool have_career;
    public bool alert_cashFrow;
    public bool alert_saving;
    public int predictMonth;
    public string career_name;
    public float career_salary;
    public AccountAmount.AccountType account_type;

    public AlertAnalytic(int level, AccountAmount.AccountType type)
    {
        importance_level= level;
        account_type= type;
    }

    public AlertAnalytic(int level, bool recommendCareer, bool have_career)
    {
        importance_level = level;
        recommend_career = recommendCareer;
        this.have_career = have_career;
    }

    public AlertAnalytic(int level, bool recommendCareer, bool have_career, string careername, float careersalary)
    {
        importance_level = level;
        recommend_career = recommendCareer;
        this.have_career = have_career;
        career_name = careername;
        career_salary = careersalary;
    }

    public AlertAnalytic(int level, bool alert_Cashflow, int predictMonth)
    {
        importance_level = level;
        alert_cashFrow = alert_Cashflow;
        this.predictMonth = predictMonth;
    }

    public AlertAnalytic(int level, bool alertSaving)
    {
        importance_level = level;
        alert_saving = alertSaving;
    }
}