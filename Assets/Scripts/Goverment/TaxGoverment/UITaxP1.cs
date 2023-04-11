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

    public void SetData()
    {
        float FI = filetax.InputFI;
        float MI = filetax.InputMI;
        float RI = filetax.InputRI;


        if (RI != 0)
        {
            inputClick1.SetActive(false);
            TextInput1.SetActive(true);

            TextInput1.GetComponent<TextMeshProUGUI>().text = RI.ToString("F");
        }

        if (MI != 0)
        {
            inputClick2.SetActive(false);
            TextInput2.SetActive(true);

            TextInput2.GetComponent<TextMeshProUGUI>().text = MI.ToString("F");
        }

        if (FI != 0) 
        {
            inputClick3.SetActive(false);
            TextInput3.SetActive(true);

            TextInput3.GetComponent<TextMeshProUGUI>().text = FI.ToString("F");
        }


    }
}
