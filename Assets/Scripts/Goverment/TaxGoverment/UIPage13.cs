using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPage13 : MonoBehaviour
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

    public GameObject Details_template;
    public Transform DetailCalculate;
    GameObject Details;

    public void SetInput()
    {
        if (float.TryParse(input_income.gameObject.GetComponent<TMP_InputField>().text, out Amount))
        {
            text.SetActive(false);
        } else
        {
            EnterBtn.interactable = false;
        }
    }

    private void Update()
    {
        if (Amount < 0)
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
        taxSystem.InputFI = Amount;
        taxSystem.FirstChangeT3 = true;
        taxP1.SetData();
        text.SetActive(true);
    }

    public void OpenClick()
    {
        if (Details == null)
        {
            OpenDetails();
        }
        else
        {
            CloseDetails();
        }
    }

    public void OpenDetails()
    {
        Details = Instantiate(Details_template, DetailCalculate);
        UICalDetails uiCalDetails = Details.GetComponent<UICalDetails>();

        taxSystem.CalculateExpense();
        uiCalDetails.SetData(taxSystem.InputRI, taxSystem.expensesRI,
            taxSystem.InputMI, taxSystem.expensesMI,
            taxSystem.InputFI, taxSystem.expensesFI);
    }

    public void CloseDetails()
    {
        Destroy(Details);
        Details = null;
    }
}
