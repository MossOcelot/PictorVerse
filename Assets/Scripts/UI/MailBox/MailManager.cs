using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MailManager : MonoBehaviour
{
    public List<Mail> myMails;
    public GameObject MailCardTemplate;
    public Transform mailList;
    public TextMeshProUGUI mailCount;
    public List<string> texts;
    int oldMailCount;

    GameObject mailCard;
    bool IsOpen = false;
    public void AddMails(Mail newMail)
    {
        myMails.Insert(0, newMail);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (IsOpen)
            {
                IsOpen = !IsOpen;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            } else
            {
                IsOpen = !IsOpen;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            
        }
    }
    private void FixedUpdate()
    {
        int count = myMails.Count;
        if(oldMailCount != count)
        {
            oldMailCount = count;
            mailCount.text = count.ToString() + "/100";
            int len = mailList.childCount;
            for(int i = 0; i < len; i++)
            {
                Destroy(mailList.GetChild(i).gameObject);
            }
            foreach(Mail mail in myMails)
            {
                mailCard = Instantiate(MailCardTemplate, mailList);
                UIMailCard uiMailCard = mailCard.GetComponent<UIMailCard>();
                uiMailCard.SetMailCard(mail.Subject, mail.Body);
                if(mail.mailType == "Tax")
                {
                    Action updateAction = mail.actionMail;
                    uiMailCard.SetActionBtn(new ActionBtn(true, "จ่าย", updateAction));
                } else
                {
                    uiMailCard.SetActionBtn(new ActionBtn(false));
                }
            }
        }
    }
}

[System.Serializable]
public class Mail
{
    public string mailType;
    public string Subject;
    public string Body;
    public int[] time;
    public Action actionMail;
    public List<InventoryItem> itemGifts;

    public Mail(string mailType, string subject, string body)
    {
        this.mailType = mailType;
        this.Subject = subject;
        this.Body = body;
        UpdateTime();
    }
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

