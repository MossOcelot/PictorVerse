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
                        mail_manager.AddMail("Loan", $"ใบแจ้งจ่ายหนี้ค้างชำระ วันที่ {date[0]}/{date[1]}/{date[2]}",
                            $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} จ่ายขั้นต่ำ {(debt + loanInterest) * 0.10f}", null, null);
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
            mail_manager.AddMail("Loan", $"ใบแจ้งค้างชำระหนี้ วันที่ {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} เนื่องจากคุณไม่ได้จ่ายมาแล้ว {round - 1} เดือน ส่งผลทำให้ธนาคารจะยึดทรัพย์สินทั้งหมดของคุณ เพื่อนำมาชำระหนี้", null, null);

            player.setMyStatic(7, 0);
            if (SeizePropertyDeposite()) return;
            if (SeizePropertyPocket()) return;
            if (SeizePropertyItemInMainBag()) return;   
            
            mail_manager.AddMail("Loan", $"ใบแจ้งเตือนผิดนัดหนี้",
                $"{player.getPlayerName()} มีหนี้ค้างชำระเหลือ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} ธนาคารได้ยึดทรัพย์สินจนหมดแล้วทำให้ไม่สามารถยื่นต่อได้ โปรดหาเงินมาชำระให้ครบไม่งั้นเดือนถัดไป จะถูกยึดอีกครั้ง", null, null);

       } 
       else if(round >= 2)
       {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", $"ใบแจ้งจ่ายค่าค้างหนี้ค้างชำระ วันที่ {date[0]}/{date[1]}/{date[2]}",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} จ่ายขั้นต่ำ {((debt + loanInterest) * 0.10f) * round } เนื่องจากคุณไม่ได้จ่ายมาแล้ว {round - 1} เดือน", null, null);
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
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน ประเภทเงินในบัญชีเงินฝาก",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากบัญชีเงินฝาก และเหลือเงินเป็นจำนวน {money_left} ในบัญชี", null, null);

            ClearLoan();
            return true;
        }

        if (deposite_amount >= loanInterest)
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน ประเภทเงินในบัญชีเงินฝาก",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากกระเป๋าทั้งหมดแล้ว แต่เงินในกระเป๋าของคุณไม่พอ ทำให้คุณยังเหลือหนี้ที่ต้องชำระอีก {debt- (deposite_amount - loanInterest)} โดยมีหนี้เงินต้น {debt - deposite_amount - loanInterest} และ ดอกเบี้ย 0", null, null);

            deposite_amount -= loanInterest;
            loanInterest = 0;
            debt -= deposite_amount;
        }
        else
        {
            
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "ประเภทเงินในบัญชีเงินฝาก",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากกระเป๋าทั้งหมดแล้ว แต่เงินในกระเป๋าของคุณไม่พอ ทำให้คุณยังเหลือหนี้ที่ต้องชำระอีก {allLoan - deposite_amount} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest - deposite_amount} ", null, null);

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
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากกระเป๋าทั้งหมดแล้ว และเงินที่เหลือจากการชำระหนี้ จำนวน {money_left} จะถูกโอนเป็นเงินสกุล <sprite index={(int)section}>", null, null);

            ClearLoan();
            return true;
        }

        if (allValue >= loanInterest)
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {allLoan} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากกระเป๋าทั้งหมดแล้ว แต่เงินในกระเป๋าของคุณไม่พอ ทำให้คุณยังเหลือหนี้ที่ต้องชำระอีก {allLoan - loanInterest} โดยมีหนี้เงินต้น {debt - (allLoan - loanInterest)} และ ดอกเบี้ย {loanInterest}", null, null);
            
            allValue -= loanInterest;
            loanInterest = 0;
            debt -= allValue;
        }
        else
        {
            UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {allLoan} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดเงินจากกระเป๋าทั้งหมดแล้ว แต่เงินในกระเป๋าของคุณไม่พอ ทำให้คุณยังเหลือหนี้ที่ต้องชำระอีก {allLoan - (loanInterest - allValue)} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest - allValue} ", null, null);

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
                mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน ประเภทไอเทมในกระเป๋า",
                    $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                    $"คุณได้ถูกยึดของจากกระเป๋าหลัก", null, null);
                
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
            mail_manager.AddMail("Loan", "แจ้งเตือนยึดทรัพย์สิน ประเภทไอเทมในกระเป๋า",
                $"{player.getPlayerName()} มีหนี้ค้างชำระ {debt + loanInterest} โดยมีหนี้เงินต้น {debt} และ ดอกเบี้ย {loanInterest} " +
                $"คุณได้ถูกยึดของจากกระเป๋าหลัก แต่ไอเทมในกระเป๋าของคุณไม่พอ ทำให้คุณยังเหลือหนี้ที่ต้องชำระอีก {allLoan} โดยมีหนี้เงินต้น {allLoan} และ ดอกเบี้ย 0", null, null);
            
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
