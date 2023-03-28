using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIMailCard : MonoBehaviour
{
    public int mail_id;
    public TextMeshProUGUI miniSubject;
    public TextMeshProUGUI miniBody;
    public ActionBtn actionBtn;
    public Button myButton;

    private UIMailPaper mailpaper;
    private MailManager mailManager;
    public string subject;
    public string body;

    private void Start()
    {
        mailpaper = GameObject.FindGameObjectWithTag("MailPaper").gameObject.GetComponent<UIMailPaper>();
        mailManager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<MailManager>();
    }
    public void SetMailCard(int mailID, string subject, string body)
    {
        this.mail_id = mailID;
        this.subject = subject;
        this.body = body;
        miniSubject.text = subject;
        miniBody.text = body;
    }

    public void SetActionBtn(ActionBtn actionBtn)
    {
        if (actionBtn != null)
        {
            this.actionBtn = actionBtn;
        }
        myButton.onClick.AddListener(() => SetActionMailCardBtn());
    }

    public void SetActionMailCardBtn()
    {
        mailpaper.SetMailPaperData(subject, body);
        if (actionBtn != null)
        {
            mailpaper.SetCorrectBtn(actionBtn.status, actionBtn.text, actionBtn.action);
        }
        mailpaper.SetDeleteBtn(true, "ลบ", ()=> DeleteCard());
    }

    private void DeleteCard()
    {
        mailpaper.SetMailPaperData("", "");
        mailpaper.SetAlert("");
        mailpaper.SetCorrectBtn(false);
        int len = mailManager.myMails.Count;
        for(int i = 0; i < len;i++)
        {
            int mailId = mailManager.myMails[i].mail_index;
            if(mailId == mail_id)
            {
                mailManager.myMails.RemoveAt(i);
            }
        }
        Destroy(gameObject);
    }
}

public class ActionBtn
{
    public bool status;
    public string text;
    public Action action;

    public ActionBtn(bool status, string text, Action action)
    {
        this.status = status;
        this.text = text;
        this.action = action;
    }

    public ActionBtn(bool status)
    {
        this.status = status;
    }
}