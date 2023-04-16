using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotiController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelToClose;
    bool isOpen = false;

    public GameObject mail_card;
    public Transform mail_content;
    private UIMailBox mail_box;

    int old_len;
    public void openPanel()
    {
        if (isOpen != true)
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
        else
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
    }

    private void Start()
    {
        mail_box = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
    }

    private void FixedUpdate()
    {
        List<Mail> mails = mail_box.GetMails();
        if(mails.Count != old_len)
        {
            if(mail_content.childCount > 0)
            {
                for(int i = 0; i < mail_content.childCount; i++)
                {
                    Destroy(mail_content.GetChild(i).gameObject);
                }
            }
            for(int i = 0; i < mails.Count; i++)
            {
                GameObject obj = Instantiate(mail_card, mail_content);
                obj.GetComponent<UINotiBox>().SetData(mails[i].mail_title);
                obj.GetComponent<Button>().AddEventListener(1, OpenMail);
                obj.transform.SetAsFirstSibling();
                if (mail_content.childCount > 5)
                {
                    Destroy(mail_content.GetChild(5).gameObject);
                }
                old_len = mails.Count;
            }
        }
    }

    private void OpenMail(int i)
    {
        UIMailBox mailBox = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
        mailBox.UI.gameObject.SetActive(true);
    }
}
