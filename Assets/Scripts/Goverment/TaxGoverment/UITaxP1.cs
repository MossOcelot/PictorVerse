using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UITaxP1 : MonoBehaviour
{
    [SerializeField]
    private FileTaxSystem filetax;

    public GameObject inputClick1;
    public GameObject inputClick2;
    public GameObject inputClick3;

    public GameObject TextInput1;
    public GameObject TextInput2;
    public GameObject TextInput3;

   
    private void FixedUpdate()
    {
        if (filetax.isClick1)
        {
            SetWrong();
        }
    }

    public void SetData()
    {
        float FI = filetax.InputFI;
        float MI = filetax.InputMI;
        float RI = filetax.InputRI;


        if (RI >= 0 && filetax.FirstChangeT1)
        {
            inputClick1.SetActive(false);
            TextInput1.SetActive(true);

            TextInput1.GetComponent<TextMeshProUGUI>().color = Color.white;
            TextInput1.GetComponent<TextMeshProUGUI>().text = RI.ToString("F");
        }

        if (MI >= 0 && filetax.FirstChangeT2)
        {
            inputClick2.SetActive(false);
            TextInput2.SetActive(true);

            TextInput2.GetComponent<TextMeshProUGUI>().color = Color.white;
            TextInput2.GetComponent<TextMeshProUGUI>().text = MI.ToString("F");
        }

        if (FI >= 0 && filetax.FirstChangeT3) 
        {
            inputClick3.SetActive(false);
            TextInput3.SetActive(true);

            TextInput3.GetComponent<TextMeshProUGUI>().color = Color.white;
            TextInput3.GetComponent<TextMeshProUGUI>().text = FI.ToString("F");
        }

        filetax.isClick1 = false;
    }

    public void SetWrong()
    {
        bool RI = filetax.CheckRI;
        bool MI = filetax.CheckMI;
        bool FI = filetax.CheckFI;

        if (!RI)
        {
            TextInput1.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if(!MI)
        {
            TextInput2.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (!FI)
        {
            TextInput3.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }
}
