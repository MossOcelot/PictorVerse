using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UICalcuTaxPanel : MonoBehaviour
{
    public TextMeshProUGUI date_late;
    public TextMeshProUGUI net_income;
    public TextMeshProUGUI tax_tier;
    public TextMeshProUGUI income;
    public TextMeshProUGUI max_income_before_tier;
    public TextMeshProUGUI tax_per;
    public TextMeshProUGUI tax_amount_tier;

    public TextMeshProUGUI sum_tax;
    public TextMeshProUGUI all_tax;
    public TextMeshProUGUI all_deduction;
    public TextMeshProUGUI net_tax;

    public GameObject LineTaxTier_template;
    GameObject LineTaxTier;

    public Transform content;


    public void SetData(int day, float net_income, float tax_tier, float max_income_before_tier, float tax_per, float tax_amount_tier, float all_deduction, List<GovermentPolicyData.IndividualRangeTax> individualRangeTax)
    {
        date_late.text = $"จำนวนวันที่ค้างชำระ {day} วัน";
        this.net_income.text = net_income.ToString("F");
        this.tax_tier.text = tax_tier.ToString();
        this.income.text = net_income.ToString("F");
        this.max_income_before_tier.text = max_income_before_tier.ToString("F");
        this.tax_per.text = $"{tax_per}%";
        this.tax_amount_tier.text = tax_amount_tier.ToString("F");

        float sumTax = (net_income - max_income_before_tier) * (tax_per / 100) + tax_amount_tier;
        this.sum_tax.text = sumTax.ToString("F");
        this.all_tax.text = sumTax.ToString("F");
        this.all_deduction.text = all_deduction.ToString("F");
        this.net_tax.text = (sumTax - all_deduction).ToString("F");
        SetLineTaxTier(individualRangeTax);
    }

    public void SetLineTaxTier(List<GovermentPolicyData.IndividualRangeTax> individualRangeTax)
    {
        foreach(GovermentPolicyData.IndividualRangeTax individual in individualRangeTax) 
        {
            string text1 = "";
            if (individual.maxIncome == 0)
            {
                text1 = $"{individual.minIncome} ขึ้นไป";
            }
            else{
                text1 = $"{individual.minIncome} - {individual.maxIncome}";
            }
           
            string text2 = $"{individual.Tax}%";
            string text3 = $"{individual.cumulative_Tax}";
            LineTaxTier = Instantiate(LineTaxTier_template, content);
            LineTaxTier.gameObject.GetComponent<UILineTaxType>().SetData(text1, text2, text3);
        }
    }

    public void Close()
    {
        int len = content.childCount;
        for(int i = 0; i < len; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
