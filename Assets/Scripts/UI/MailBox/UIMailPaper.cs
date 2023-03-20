using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIMailPaper : MonoBehaviour
{
    public TextMeshProUGUI Subject;
    public TextMeshProUGUI Body;
    public TextMeshProUGUI alert;
    public Button CorrectBtn;
    public Button DeleteBtn;

    public void SetMailPaperData(string SubjectText, string BodyText)
    {
        Subject.text = SubjectText;
        Body.text = BodyText;
    }

    public void SetCorrectBtn(bool status, string nameBtn, Action action) 
    { 
        CorrectBtn.gameObject.SetActive(status);
        CorrectBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = nameBtn;
        CorrectBtn.onClick.AddListener(() => action());
    }

    public void SetCorrectBtn(bool status)
    {
        CorrectBtn.gameObject.SetActive(status);
    }
    public void SetDeleteBtn(bool status, string nameBtn, Action action)
    {
        DeleteBtn.gameObject.SetActive(status);
        DeleteBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = nameBtn;
        DeleteBtn.onClick.AddListener(() => action());
    }

    public void SetDeleteBtn(bool status)
    {
        DeleteBtn.gameObject.SetActive(status);
    }

    public void SetAlert(string text)
    {
        alert.gameObject.SetActive(true);
        alert.text = text;
    }

    
}
