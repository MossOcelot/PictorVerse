using inventory.Model;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public List<Mail> myMails;
    public GameObject MailCardTemplate;
    public Transform mailList;
    int oldMailCount;

    GameObject mailCard;
    public void AddMails(Mail newMail)
    {
        myMails.Insert(0, newMail);
    }

    public void FixedUpdate()
    {
        int count = myMails.Count;
        if(oldMailCount != count)
        {
            oldMailCount = count;
            int len = mailList.childCount;
            for(int i = 0; i < len; i++)
            {
                Destroy(mailList.GetChild(i).gameObject);
            }
            foreach(Mail mail in myMails)
            {
                mailCard = Instantiate(MailCardTemplate, mailList);
                UIMailCard uiMailCard = mailCard.GetComponent<UIMailCard>();
                uiMailCard.SetMailCard(mail.mail_index,mail.Subject, mail.Body);
                if(mail.mailType == "Tax")
                {
                    Action updateAction = mail.actionMail;
                    uiMailCard.SetActionBtn(new ActionBtn(true, "รับ", updateAction));
                }
            }
        }
    }
}

[System.Serializable]
public class Mail : MonoBehaviour
{
    public int mail_index => GetInstanceID();
    public string mailType;
    public string Subject;
    public string Body;
    public int[] time;
    public Action actionMail;
    public List<InventoryItem> itemGifts;

    public Mail (string mailType, string subject, string body, Action action)
    {
        this.mailType = mailType;
        this.Subject = subject;
        this.Body = body;
        this.actionMail = action;
        UpdateTime();
    }

    public Mail (string mailType, string subject, string body, Action action, List<InventoryItem> items)
    {
        this.mailType = mailType;
        this.Subject = subject;
        this.Body = body;
        this.actionMail = action;
        this.itemGifts = items;
        UpdateTime();
    }

    public void UpdateTime()
    {
        Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        time = date.getDateTime();
    }
}

