using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoanPlayerController : MonoBehaviour
{
    Timesystem TimeSystem;
    public InventorySO mainBagPlayer;
    [SerializeField]
    private float debt;
    [SerializeField]
    private float loanInterest;
    public bool HaveDept;
    public int[] timer;

    public bool IsSent;

    public int round;

    private SceneStatus.section section;

    PlayerStatus player;
    AllItemInMarket allItemInMarket;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        TimeSystem = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
        allItemInMarket = GameObject.FindGameObjectWithTag("AllMarket").gameObject.GetComponent<AllItemInMarket>();
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
            mail_manager.AddMail("Loan", $"��駤�ҧ����˹�� �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ���ͧ�ҡ�س�������������� {round - 1} ��͹ �觼ŷ���鸹Ҥ�è��ִ��Ѿ���Թ�������ͧ�س ���͹��Ҫ���˹��", null, null);

            player.setMyStatic(7, 0);
            if (SeizePropertyDeposite()) return;
            if (SeizePropertyPocket()) return;
            if (SeizePropertyItemInMainBag()) return;   
            
            mail_manager.AddMail("Loan", $"�����͹�Դ�Ѵ˹��",
                $"{player.getPlayerName()} ��˹���ҧ��������� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ��Ҥ�����ִ��Ѿ���Թ��������Ƿ�����������ö��蹵���� �ô���Թ�Ҫ������ú�������͹�Ѵ� �ж١�ִ�ա����", null, null);

       } 
       else if(round >= 2)
       {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", $"��駨��¤�Ҥ�ҧ˹���ҧ���� �ѹ��� {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} ���¢�鹵�� {((debt + loanInterest) * 0.10f) * round } ���ͧ�ҡ�س�������������� {round - 1} ��͹", null, null);
       }
    }

    private bool SeizePropertyDeposite()
    {
        float allLoan = debt + loanInterest;
        AccountBankOperation accontOperation = GameObject.FindGameObjectWithTag("AccountOperation").gameObject.GetComponent<AccountBankOperation>();
        float deposite_amount = accontOperation.GetDeposit(player.GetAccountID());
        if (deposite_amount == 0) return false;
        if (deposite_amount >= allLoan)
        {
            float money_left = deposite_amount - allLoan;
            accontOperation.SetDeposit(player.GetAccountID(), money_left);
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ �������Թ㹺ѭ���Թ�ҡ",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�ѭ���Թ�ҡ ���������Թ�繨ӹǹ {money_left} 㹺ѭ��", null, null);

            ClearLoan();
            return true;
        }

        if (deposite_amount >= loanInterest)
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ �������Թ㹺ѭ���Թ�ҡ",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�����ҷ��������� ���Թ㹡����Ңͧ�س���� �����س�ѧ�����˹�����ͧ�����ա {debt- (deposite_amount - loanInterest)} ����˹���Թ�� {debt - deposite_amount - loanInterest} ��� �͡���� 0", null, null);

            deposite_amount -= loanInterest;
            loanInterest = 0;
            debt -= deposite_amount;
        }
        else
        {
            
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "�������Թ㹺ѭ���Թ�ҡ",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�����ҷ��������� ���Թ㹡����Ңͧ�س���� �����س�ѧ�����˹�����ͧ�����ա {allLoan - deposite_amount} ����˹���Թ�� {debt} ��� �͡���� {loanInterest - deposite_amount} ", null, null);

            loanInterest -= deposite_amount;
        }
        accontOperation.SetDeposit(player.GetAccountID(), 0);
        return false;
    }

    private bool SeizePropertyPocket()
    {
        float allLoan = debt + loanInterest;
        float allValue = 0;
        List<float> costs = player.player_accounts.getPocketList();
        for(int i = 0; i < costs.Count; i++)
        {
            float value = costs[i];
            if(i != (int)section)
            {
                float rate = new ExchangeRate().getExchangeRate(i, (int)section);
                value = costs[i] * rate;
            }
            player.player_accounts.SetPocketList(i, 0);
            allValue += value;
        }
        if(allValue >= allLoan)
        {
            float money_left = allValue - allLoan;
            float newValue = player.player_accounts.getPocket()[section.ToString()] + money_left;
            player.player_accounts.setPocket(section.ToString(), newValue);
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�����ҷ��������� ����Թ�������ͨҡ��ê���˹�� �ӹǹ {money_left} �ж١�͹���Թʡ�� <sprite index={(int)section}>", null, null);

            ClearLoan();
            return true;
        }

        if (allValue >= loanInterest)
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ",
                $"{player.getPlayerName()} ��˹���ҧ���� {allLoan} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�����ҷ��������� ���Թ㹡����Ңͧ�س���� �����س�ѧ�����˹�����ͧ�����ա {allLoan - loanInterest} ����˹���Թ�� {debt - (allLoan - loanInterest)} ��� �͡���� {loanInterest}", null, null);
            
            allValue -= loanInterest;
            loanInterest = 0;
            debt -= allValue;
        }
        else
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ",
                $"{player.getPlayerName()} ��˹���ҧ���� {allLoan} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�Թ�ҡ�����ҷ��������� ���Թ㹡����Ңͧ�س���� �����س�ѧ�����˹�����ͧ�����ա {allLoan - (loanInterest - allValue)} ����˹���Թ�� {debt} ��� �͡���� {loanInterest - allValue} ", null, null);

            loanInterest -= allValue;
        }
        return false;
    }

    private bool SeizePropertyItemInMainBag()
    {
        Dictionary<int, InventoryItem> items = mainBagPlayer.GetCurrentInventoryState();
        float allLoan = debt + loanInterest;
        foreach (int index in items.Keys)
        {
            int quantityItems = items[index].quantity;
            float priceItems = (quantityItems * allItemInMarket.GetitemsInMarket()[items[index].item].price);
            if (priceItems >= allLoan)
            {
                float money_left = priceItems - allLoan;
                int quantity = Mathf.FloorToInt(money_left / allItemInMarket.GetitemsInMarket()[items[index].item].quantity);
                int quantitySell = quantityItems - quantity;
                mainBagPlayer.RemoveItem(index, quantity);

                UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
                mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ ����������㹡�����",
                    $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                    $"�س��١�ִ�ͧ�ҡ��������ѡ", null, null);
                
                ClearLoan();
                return true;
            } else
            {
                mainBagPlayer.RemoveItem(index, quantityItems);
                allLoan -= priceItems;
            }
        }

        if(allLoan > 0)
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "����͹�ִ��Ѿ���Թ ����������㹡�����",
                $"{player.getPlayerName()} ��˹���ҧ���� {debt + loanInterest} ����˹���Թ�� {debt} ��� �͡���� {loanInterest} " +
                $"�س��١�ִ�ͧ�ҡ��������ѡ ������㹡����Ңͧ�س���� �����س�ѧ�����˹�����ͧ�����ա {allLoan} ����˹���Թ�� {allLoan} ��� �͡���� 0", null, null);
            
            debt = allLoan;
            loanInterest = 0;
        }
        return false;
    }

    private void ClearLoan() 
    {
        int[] date = TimeSystem.getDateTime();
        debt = 0;
        loanInterest = 0;
        round = 0;
        HaveDept = false;
        timer = new int[] { date[0], date[1], date[2] };
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
