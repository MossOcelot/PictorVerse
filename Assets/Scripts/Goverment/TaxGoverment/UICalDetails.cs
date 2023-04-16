using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICalDetails : MonoBehaviour
{
    public TextMeshProUGUI RI_text;
    public TextMeshProUGUI RI_text2;
    public TextMeshProUGUI RI_text3;
    public TextMeshProUGUI RI_text4;
    public TextMeshProUGUI RI_text5;

    public TextMeshProUGUI MI_text;
    public TextMeshProUGUI MI_text2;
    public TextMeshProUGUI MI_text3;
    public TextMeshProUGUI MI_text4;
    public TextMeshProUGUI MI_text5;

    public TextMeshProUGUI FI_text;
    public TextMeshProUGUI FI_text2;
    public TextMeshProUGUI FI_text3;
    public TextMeshProUGUI FI_text4;
    public TextMeshProUGUI FI_text5;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }
    public void SetData(float RI_Income, float RI_expense, 
                        float MI_Income, float MI_expense, 
                        float FI_Income, float FI_expense )
    {
        RI_text.text = RI_Income.ToString("F");
        RI_text2.text = RI_Income.ToString("F");
        RI_text3.text = RI_expense.ToString("F");
        RI_text4.text = (RI_Income - RI_expense).ToString("F");
        RI_text5.text = (RI_Income - RI_expense).ToString("F");

        MI_text.text = MI_Income.ToString("F");
        MI_text2.text = MI_Income.ToString("F");
        MI_text3.text = MI_expense.ToString("F");
        MI_text4.text = (MI_Income - MI_expense).ToString("F");
        MI_text5.text = (MI_Income - MI_expense).ToString("F");

        FI_text.text = FI_Income.ToString("F");
        FI_text2.text = FI_Income.ToString("F");
        FI_text3.text = FI_expense.ToString("F");
        FI_text4.text = (FI_Income - FI_expense).ToString("F");
        FI_text5.text = (FI_Income - FI_expense).ToString("F");
    }
}
