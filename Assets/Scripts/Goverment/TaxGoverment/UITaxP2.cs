using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaxP2 : MonoBehaviour
{
    [SerializeField]
    private FileTaxSystem fileTax;
    public GameObject input_Endownment;
    public float AmountEndowment;

    public GameObject input_HealthInsurance;
    public float AmountHealthInsurance;

    public Button NextBtn;
    public void SetInputEndownment()
    {
        if (float.TryParse(input_Endownment.gameObject.GetComponent<TMP_InputField>().text, out AmountEndowment) || input_Endownment.gameObject.GetComponent<TMP_InputField>().text == "")
        {
            fileTax.InputEndownment = AmountEndowment;
            NextBtn.interactable = true;
        }
        else 
        {
            NextBtn.interactable = false;
        }
    }

    public void SetInputHealthInsurance()
    {
        if (float.TryParse(input_HealthInsurance.gameObject.GetComponent<TMP_InputField>().text, out AmountHealthInsurance) || input_HealthInsurance.gameObject.GetComponent<TMP_InputField>().text == "")
        {
            fileTax.InputHeathInsurance = AmountHealthInsurance;
            NextBtn.interactable = true;
        }
        else
        {
            NextBtn.interactable = false;
        }
    }
}
