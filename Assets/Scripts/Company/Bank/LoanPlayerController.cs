using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int round;
    private void Start()
    {
        TimeSystem = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
    }

    private void FixedUpdate()
    {
        if (!HaveDept) return;
        int[] date = TimeSystem.getDateTime();
        if (date[2] >= timer[2] && (date[2] != 0 || timer[2] != 0))
        {
            if (date[1] >= timer[1] && (date[1] != 0 || timer[1] != 0))
            {
                if (date[0] == timer[0] && (date[0] != 0 || timer[0] != 0))
                {
                    if (IsSent) return;
                    loanInterest += (debt + loanInterest) * 0.0167f;
                    if(round == 0)
                    {
                        Debug.Log("Round Round");
                        UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
                        PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
                        mail_manager.AddMail("Loan", $"��駨���˹���ҧ���� �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                            $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ���¢�鹵�� {(debt + loanInterest) * 0.10f}", null, null);
                    }
                    round++;
                    CheckDefaultStatus(date);
                    IsSent = true;
                }
                else
                {
                    IsSent = false;
                }
            }
        }
        
    }

    private void CheckDefaultStatus(int[] date)
    {
       if(round > 4)
       {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
            mail_manager.AddMail("Loan", $"��駤�ҧ����˹�� �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ���¢�鹵�� {((debt + loanInterest) * 0.10f) * round} ���ͧ�ҡ�س�������������� {round - 1} ��͹ �觼ŷ���鸹Ҥ�è��ִ��Ѿ���Թ�������ͧ�س ���͹��Ҫ���˹��", null, null);
        } 
       else if(round >= 2)
       {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
            mail_manager.AddMail("Loan", $"��駨��¤�Ҥ�ҧ˹���ҧ���� �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ���¢�鹵�� {((debt + loanInterest) * 0.10f) * round } ���ͧ�ҡ�س�������������� {round - 1} ��͹", null, null);
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
