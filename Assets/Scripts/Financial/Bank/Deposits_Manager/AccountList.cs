using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountList
{
    public int[] date = new int[] { };
    public string accounts_name;
    public float deposit;
    public float withdrawal;
    public float balance;
    public string number;

    public AccountList(int[] date, string accounts_name, float deposit, float withdrawal, float balance, string number) 
    {
        this.date = date;
        this.accounts_name= accounts_name;
        this.deposit = deposit;
        this.withdrawal = withdrawal;
        this.balance = balance;
        this.number = number;
    }
}
