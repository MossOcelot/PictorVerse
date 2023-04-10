using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PolicyGoverment", menuName = "Goverment/GovermentPolicy")]
public class GovermentPolicyData : ScriptableObject
{
    [System.Serializable]
    public class IndividualRangeTax
    {
        public float minIncome;
        public float maxIncome;
        public float Tax;
    }

    public int vat_tax;
    public int business_tax;
    public List<IndividualRangeTax> individual_tax;
    public int travel_tax;
    public int vehicle_tax;
    public int personal_star_tax;
    public float limitLessExpenses;

    public int[] taxCollectionDay;
}
