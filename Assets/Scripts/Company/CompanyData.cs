using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Company", menuName = "Company/CompanyStatus")]
public class CompanyData : ScriptableObject
{
    public string nameCompany;
    public PocketDetails pocketCompany;
    public Financial_Details financialDetail;
    public List<AccountsDetail> accountDetailCompany;
}
