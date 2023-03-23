using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIInsuraceCard : MonoBehaviour
{
    [SerializeField] 
    private Image logo;
    [SerializeField]
    private InsuranceItems insurance;

    public TextMeshProUGUI Title_text;

    public void SetInsurance(InsuranceItems newInsurance)
    {
        this.insurance = newInsurance;
    }
    public void SetData(Sprite logo, string name)
    {
        this.logo.sprite = logo;
        Title_text.text = name;
    }
}
