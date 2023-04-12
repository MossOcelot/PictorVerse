using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICalcuTaxPanel : MonoBehaviour
{
    public TextMeshProUGUI date_late;
    public TextMeshProUGUI net_income;
    public TextMeshProUGUI tax_tier;
    public TextMeshProUGUI income;
    public TextMeshProUGUI expense;
    public TextMeshProUGUI tax_per;
    public TextMeshProUGUI tax_amount_tier;

    public TextMeshProUGUI sum_tax;
    public TextMeshProUGUI all_tax;
    public TextMeshProUGUI all_deduction;
    public TextMeshProUGUI net_tax;

    public GameObject LineTaxTier_template;
    GameObject LineTaxTier;

    public Transform content;


    public void SetData()
    {

    }
}
