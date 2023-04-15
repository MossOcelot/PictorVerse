using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UILineTaxType : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public void SetData(string range_amount, string tax_per, string amount)
    {
        text1.text = range_amount; text2.text = tax_per; text3.text = tax_per;
    }
}
