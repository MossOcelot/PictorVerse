using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareerPlayer : MonoBehaviour
{
    public CareerSO Career;
    public int CareerScore;

    public bool finishDailyQuest;
    public bool HaveCareer;
    public int absenteeismPerDay;
    public int absenteeismPerMonth;

    int DayInGame;
    public PlayerStatus playerStatus;
    Timesystem time_system;


    public int finishDailyQuestInADay;

    int present_day;
    int present_month;  
    // Start is called before the first frame update
    private void Start()
    {
        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();

        int[] Now_time = time_system.getDateTime();
        DayInGame = time_system.daysPerMonth;
        present_day = Now_time[0];
        present_month = Now_time[1];
    }

    private void Update()
    {
        int[] Now_time = time_system.getDateTime();

        Debug.Log("Now_Time: " + Now_time[0]);
        if(present_day != Now_time[0])
        {
            CheckFinishDailyQuestInDay();
            CheckAbsenteeismPerDay();
            present_day = Now_time[0];
        }

        if(present_month != Now_time[1])
        {
            SpendSalary();
            ChecksenteeismPerMonth();
            present_month = Now_time[1];
        }
    }
    public void SetCareer(CareerSO Career)
    {
        this.Career = Career;
    }

    public void RemoveCareer()
    {
        this.Career = null;
        finishDailyQuest = false;
        HaveCareer = false;

        absenteeismPerDay = 0;
        absenteeismPerMonth = 0;
        finishDailyQuestInADay = 0;
    }
    
    private void CheckAbsenteeismPerDay()
    {
        if (!finishDailyQuest)
        {
            absenteeismPerDay++;
        }
        finishDailyQuest = false;
    }

    private void ChecksenteeismPerMonth()
    {
        if(absenteeismPerDay > 2)
        {
            absenteeismPerMonth++;
            absenteeismPerDay = 0;
        }

        if(absenteeismPerMonth > 3)
        {
            RemoveCareer();
            absenteeismPerMonth = 0;
        }
    }

    private void SpendSalary()
    {
        float Salary = Career.salary + CareerScore;
        float True_Salary = Salary * (((float)DayInGame - (float)absenteeismPerDay) / (float)DayInGame);

        float new_Amount = playerStatus.player_accounts.getPocket()[Career.section_workplace.ToString()];
        playerStatus.player_accounts.setPocket(Career.section_workplace.ToString(), new_Amount);

        int[] Now_time = time_system.getDateTime();
        AccountsDetail account_Player = new AccountsDetail() { date = Now_time, accounts_name = "เงินเดือน", account_type = "income", income = True_Salary, expense = 0 };
        playerStatus.addAccountsDetails(account_Player);

        ResetData();
    }

    private void CheckFinishDailyQuestInDay()
    {
        if (Career == null) return;
        int amountDailyQ = Career.AmountDailyQuest;

        if (finishDailyQuestInADay >= Mathf.RoundToInt(amountDailyQ * 0.8f))
        {
            finishDailyQuest = true;
            finishDailyQuestInADay = 0;
        }
    }

    private void ResetData()
    {
        CareerScore = 0;
    }
}