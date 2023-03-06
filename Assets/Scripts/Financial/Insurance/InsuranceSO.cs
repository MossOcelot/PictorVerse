using inventory.Model;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InsuranceSO", menuName = "Insurance")]
public class InsuranceSO : ScriptableObject
{
    public enum insurance_type { endowment, health_insurance }

    public string insurance_name;
    public insurance_type insurance;
    public float insurance_limit;
    public float insurance_percent;
}

[System.Serializable]
public class Suppackage {
    public int year;
    public float price;
}

[System.Serializable]
public struct InsuranceItems
{
    public InsuranceSO insurance;
    public List<Suppackage> subpackage;
    public int[] buyDay;
    public int[] expireDay;
    public InsuranceItems ChangeSubPackage(string command, int year, float price)
    {
        List<Suppackage> newSubpackage = this.subpackage;
        if(command == "add")
        {
            newSubpackage.Add(new Suppackage() { year = year, price = price });
        } else if (command == "update")
        {
            foreach(Suppackage pack in newSubpackage)
            {
                if(pack.year == year)
                {
                    pack.price = price;
                }
            }
        }
        return new InsuranceItems
        {
            insurance = this.insurance,
            subpackage = newSubpackage,
            buyDay = this.buyDay,
            expireDay = this.expireDay,
        };
    }

    public InsuranceItems ChangeBuyDay(int[] newBuyDay)
    {
        Debug.Log("Insurance: " + this.insurance);
        return new InsuranceItems
        {
            insurance = this.insurance,
            subpackage = this.subpackage,
            buyDay = newBuyDay,
            expireDay = this.expireDay,
        };
    }

    public InsuranceItems ChangeExpireDay(int[] newExpireDay)
    {
        return new InsuranceItems
        {
            insurance = this.insurance,
            subpackage = this.subpackage,
            buyDay = this.buyDay,
            expireDay = newExpireDay,
        };
    }
}