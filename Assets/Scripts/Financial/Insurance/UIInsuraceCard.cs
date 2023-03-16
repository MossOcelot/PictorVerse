using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIInsuraceCard : MonoBehaviour
{
    [SerializeField]
    private InsuranceItems insurance;

    public TextMeshProUGUI Title_text;

    public void SetInsurance(InsuranceItems newInsurance)
    {
        this.insurance = newInsurance;
    }
    public void SetData(string name)
    {
        Title_text.text = name;
    }
}
