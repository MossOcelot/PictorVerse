using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIOutMoney : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_MyPocket;
    [SerializeField]
    private TextMeshProUGUI text_Tax;

    public void SetData(float pocket, float tax)
    {
        text_MyPocket.text = pocket.ToString("F");
        text_Tax.text = tax.ToString("F");
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
