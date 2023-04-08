using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goverment", menuName = "Goverment/GovermentStatus")]
public class GovermentStatusData : ScriptableObject
{
    public enum section { section1, section2, section3, section4, section5 }

    public string name_goverment;
    public section govermentInSection;
    public List<AccountsDetail> govermentAccounts;
    public Financial_Details govermentFinancial;
    public PocketDetails govermentPockets;

}
