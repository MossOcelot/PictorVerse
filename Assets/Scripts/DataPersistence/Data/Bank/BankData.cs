using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankData
{
    public float BeforePolicyInterestRate;

    public PocketDetails Bank_Pocket;
    public List<AccountsDetail> Bank_accounts;
    public Financial_Details Bank_Financial;

    public List<InsuranceItems> endowments;
    public List<InsuranceItems> health_insurances;

    public List<AccountData.Accounts> accountDB;

    public int[] interestDate;
    public bool IsActive;
    public BankData(BankCompany bankcompany)
    {
        GameObject bankData = bankcompany.Bank.gameObject;
        BeforePolicyInterestRate = bankData.GetComponent<Bank_Policy>().GetBeforePolicyInterestRate();

        Bank_Status bank_status = bankData.GetComponent<Bank_Status>();
        Bank_Pocket = bank_status.GetBankPocket();
        Bank_accounts = bank_status.GetBank_Accounts();
        Bank_Financial = bank_status.GetBankAllFinancial();

        CallInsurancePage bank_insurance = bankData.GetComponent<CallInsurancePage>();
        endowments = bank_insurance.GetEndowments();
        health_insurances = bank_insurance.GetHealth_insurances();

        Bank_Account_DB account_db = bankData.transform.GetChild(1).gameObject.GetComponent<Bank_Account_DB>();
        accountDB = account_db.GetAllAccount();

        Bank_Account_manager account_manager = bankData.transform.GetChild(1).gameObject.GetComponent<Bank_Account_manager>();
        interestDate = account_manager.GetInterestDate();
        IsActive = account_manager.GetIsActive();   
    }

}
