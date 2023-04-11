using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPage11 : MonoBehaviour
{
    [SerializeField] 
    private FileTaxSystem taxSystem;
    [SerializeField]
    private UITaxP1 taxP1;
    [SerializeField]
    private GameObject input_income;

    public GameObject text;
    public Button EnterBtn;
    public float Amount;

    public void SetInput()
    {
        if (float.TryParse(input_income.gameObject.GetComponent<TMP_InputField>().text, out Amount))
        {
            text.SetActive(false);
        }
    }

    private void Update()
    {
        if(Amount <= 0)
        {
            EnterBtn.interactable = false;
        }
        else
        {
            EnterBtn.interactable = true;
        }
    }

    public void Enter()
    {
        taxSystem.InputRI = Amount;
        taxP1.SetData();
        text.SetActive(true);
    }
}
