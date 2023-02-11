using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralBankStatus : MonoBehaviour
{
    public enum section { section1, section2, section3, section4, section5 }

    public string bank_name;
    public section govermentInSection;
    public PocketDetails bankPocket;
    public PocketDetails foreignExchangeReserves;
    [SerializeField]
    private List<AccountsDetail> BankAccounts;


    public List<AccountsDetail> getBankAccounts()
    {
        return this.BankAccounts;
    }

    public void addBankAccounts(AccountsDetail accountDetail)
    {
        this.BankAccounts.Insert(0, accountDetail);
    }
}
