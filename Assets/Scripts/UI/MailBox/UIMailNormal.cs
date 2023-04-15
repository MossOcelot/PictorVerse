using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMailNormal : MonoBehaviour
{
    public TextMeshProUGUI head_txt;
    public TextMeshProUGUI description_txt;
    public TextMeshProUGUI date_txt;
    public TextMeshProUGUI alert_txt;

    public Button CorrectBtn;
    public Button DeleteBtn;

    public void SetData(string head, string description, string date)
    {
        head_txt.text = head;
        description_txt.text = description;
        date_txt.text = date;
    }

    public void SetAlert(string title)
    {
        alert_txt.gameObject.SetActive(true);
        alert_txt.text = title;
    }

    public void SetCorrectBtn(bool status)
    {
        alert_txt.gameObject.SetActive(false);
        CorrectBtn.gameObject.SetActive(status);
    }
}
