using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BankCompany : MonoBehaviour
{
    public GameObject Bank;
    public GameObject Insurance;
    public string BankName;
    public void Open()
    {
        Bank.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Bank.gameObject.GetComponent<Bank_Status>().SetBank_name(BankName);
    }

    public void Close()
    {
        Save();
        Bank.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Insurance.SetActive(false);
    }

    private void Awake()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    // ------------ save and load ------------
    public void Save()
    {
        SaveBankSystem.SaveBankCompany(this);
    }

    public void Load()
    {
        BankData data = SaveBankSystem.LoadBankCompany(BankName);

        if (data != null)
        {
            Bank.gameObject.GetComponent<Bank_Policy>().SetBeforePolicyInterestRate(data.BeforePolicyInterestRate);
            Bank_Status bankStatus = Bank.gameObject.GetComponent<Bank_Status>();
            bankStatus.SetBankPocket(data.Bank_Pocket);
            bankStatus.LoadBankAccounts(data.Bank_accounts);
            bankStatus.SetBank_Financial("balance", data.Bank_Financial.balance);
            bankStatus.SetBank_Financial("debt", data.Bank_Financial.debt);

            CallInsurancePage bankInsurance = gameObject.GetComponent<CallInsurancePage>();
            bankInsurance.LoadEndowments(data.endowments);
            bankInsurance.LoadHealth_insurances(data.health_insurances);

            Bank_Account_DB account_db = Bank.transform.GetChild(1).gameObject.GetComponent<Bank_Account_DB>();
            account_db.LoadAllAccount(data.accountDB);

            Bank_Account_manager account_manager = Bank.transform.GetChild(1).gameObject.GetComponent<Bank_Account_manager>();
            account_manager.LoadInterestDate(data.interestDate);
            account_manager.LoadIsActive(data.IsActive);
        }
    }
}
