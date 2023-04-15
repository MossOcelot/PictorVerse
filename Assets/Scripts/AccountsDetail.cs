using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountsDetail
{
    public int[] date = new int[] { };
    public string accounts_name = "";
    public string account_type = "";
    public float income = 0;
    public float expense = 0;
    public SceneStatus.section currencyIncome_Type;
    public SceneStatus.section currencyExpense_Type;
}
