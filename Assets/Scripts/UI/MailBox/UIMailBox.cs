using inventory.Model;
using JetBrains.Annotations;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMailBox : MonoBehaviour
{
    public GameObject MailCard_Template;
    public Transform MailCard_content;

    public GameObject MailPaper_Template;
    public Transform MailPaper_content;

    public Transform UI;
    [SerializeField]
    private List<Mail> mails = new List<Mail>();

    GameObject mail_card;
    GameObject mail_paper;
    int old_len;

    public int count_index;
    public void AddMail(string type, string mail_title, string mail_content,Action action, List<InventoryItem> giftItems)
    {
        count_index++;
        Mail newMail = new Mail(count_index, type, mail_title, mail_content, action, giftItems);
        mails.Insert(0, newMail);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(UI.gameObject.activeSelf)
            {
                UI.gameObject.SetActive(false);
            } else
            {
                UI.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UISettingBar settingBar = GameObject.FindGameObjectWithTag("SettingBar").gameObject.GetComponent<UISettingBar>();
            if (UI.gameObject.activeSelf)
            {
                UI.gameObject.SetActive(false);
                settingBar.CanExit = false;
            }
        }
    }

    private void FixedUpdate()
    {
        int len = mails.Count;
        if (len != old_len)
        {
            if(MailCard_content.childCount > 0)
            {
                for(int i = 0; i < MailCard_content.childCount; i++)
                {
                    Destroy(MailCard_content.GetChild(i).gameObject);
                }
            }
            foreach(Mail mail in mails)
            {
                mail_card = Instantiate(MailCard_Template, MailCard_content);
                UIMailCard uicard = mail_card.GetComponent<UIMailCard>();
                uicard.SetData(mail.mail_title, mail.mail_content);
                uicard.clickBtn.onClick.AddListener(() => AddMailPaper(mail));
            }
            old_len = len;
        }
    }

    private void AddMailPaper(Mail mail)
    {
        int count = MailPaper_content.childCount;
        if (count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                Destroy(MailPaper_content.GetChild(i).gameObject);
            }
        }
        mail_paper = Instantiate(MailPaper_Template, MailPaper_content);
        UIMailNormal mailNormail = mail_paper.gameObject.GetComponent<UIMailNormal>();
        int[] date = mail.date;
        string date_text = $"{date[0]}/{date[1]}/{date[2]} {date[3]}:{date[4]}";
        mailNormail.SetData(mail.mail_title, mail.mail_content, date_text);
        if(mail.action != null)
        {
            mailNormail.CorrectBtn.onClick.AddListener(() => mail.action());
            if(mail.type == "Tax")
            {
                mailNormail.CorrectBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "จ่าย";
            } 
            else
            {
                mailNormail.CorrectBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "รับ";
            }
        } else
        {
            mailNormail.CorrectBtn.gameObject.SetActive(false);
        }
        mailNormail.DeleteBtn.onClick.AddListener(() => DeleteMail(mail.index));
    }

    public List<Mail> GetMails()
    {
        return mails;
    }

    private void DeleteMail(int id)
    {
        for(int i = 0; i < mails.Count; i++)
        {
            if(mails[i].index == id)
            {
                GameObject paper = GameObject.FindGameObjectWithTag("MailPaper").gameObject;
                Destroy(paper);
                Destroy(MailCard_content.GetChild(i).gameObject);
                mails.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class Mail
{
    public int index;
    public string type;
    public string mail_title;
    public string mail_content;
    public int[] date;
    public Action action;
    public List<InventoryItem> GiftItems;

    public Mail(int index,string type, string mail_title, string mail_content, Action action, List<InventoryItem> giftItems)
    {
        this.index = index;
        this.type = type;
        this.mail_title = mail_title;
        this.mail_content = mail_content;
        this.date = GetDate();
        this.action = action;
        GiftItems = giftItems;
    }

    private int[] GetDate()
    {
        return GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
    }
}