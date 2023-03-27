using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanPlayerController : MonoBehaviour
{
    Timesystem TimeSystem;
    [SerializeField]
    private float debt;
    [SerializeField]
    private float loanInterest;
    public bool HaveDept;
    public int[] timer;

    public bool IsSent;
    private void Start()
    {
        TimeSystem = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
    }

    private void FixedUpdate()
    {
        if (!HaveDept) return;
        int[] date = TimeSystem.getDateTime();
        if (date[2] == timer[2])
        {
            if (date[1] == timer[1])
            {
                if (date[0] == timer[0])
                {
                    if (IsSent) return;
                    loanInterest += (debt + loanInterest) * 0.0167f;
                    MailManager mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<MailManager>();
                    PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
                    Mail newMail = new Mail("Loan", $"ใบแจ้งจ่ายหนี้ค้างชำระ วันที่ {date[0]}/{date[1]}/{date[2]}",
                        $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} จ่ายขั้นต่ำ {(debt + loanInterest) * 0.10f}", null);
                    mail_manager.AddMails(newMail);
                    IsSent = true;
                }
                else
                {
                    IsSent = false;
                }
            }
        }
    }
    public float GetDept()
    {
        return debt;
    }

    public void SetDebt(float newDebt)
    {
        debt = newDebt;
    }

    public float GetloanInterest()
    {
        return loanInterest;
    }

    public void SetloanInterest(float newloanInterest)
    {
        loanInterest = newloanInterest;
    }

    public float SumDept()
    {
        return debt + loanInterest;
    }

}
