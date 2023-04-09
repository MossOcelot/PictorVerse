using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GovermentPolicy : MonoBehaviour
{
    public GovermentPolicyData govermentPolicy;
    public GovermentStatus govermentStatus;


    Timesystem time_system;

    public bool IsSent;
    public int getVat()
    {
        return govermentPolicy.vat_tax;
    }

    public List<GovermentPolicyData.IndividualRangeTax> getIndividualTax()
    {
        return govermentPolicy.individual_tax;
    }
    // Start is called before the first frame update
    void Awake()
    {
        Dictionary<string, dynamic> vat_check = new VATInGalaxy().checkvat(govermentStatus.goverment.govermentInSection.ToString());

        govermentPolicy.vat_tax = vat_check["vat_tax"];
        govermentPolicy.business_tax = vat_check["business_tax"];
        govermentPolicy.travel_tax = vat_check["travel_tax"];
        govermentPolicy.vehicle_tax = vat_check["vehicle_tax"];
        govermentPolicy.personal_star_tax = vat_check["personal_star_tax"];

        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.AddComponent<Timesystem>();
    }

    private void FixedUpdate()
    {
        int[] date = time_system.getDateTime();
        if (date[2] == govermentPolicy.taxCollectionDay[2])
        {
            if (date[1] == govermentPolicy.taxCollectionDay[1])
            {
                if (date[0] == govermentPolicy.taxCollectionDay[0])
                {
                    if (IsSent) return;
                    MailManager mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<MailManager>();
                    PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
                    Mail newMail = new Mail("Tax", $"ใบแจ้งภาษี วันที่ {date[0]}/{date[1]}/{date[2]}",
                        $"{player.getPlayerName()} มีรายได้ปีที่ผ่านมาอยู่ที่ {player.GetIncomeAllYear()} ต้องเสียภาษีจำนวน {player.PayTaxes()}", ()=> TaxReport(player));
                    mail_manager.AddMails(newMail);
                    Mail newMails = new Mail("xxx", $"asdasdasd",
                        $"asdasdasdasdsad");
                    mail_manager.AddMails(newMails);
                    govermentPolicy.taxCollectionDay[2]++;
                    IsSent = true;
                } else
                {
                    IsSent = false;
                }
            }
        }
    }

    private void TaxReport(PlayerStatus playerStatus)
    {
        UIMailPaper uiMailPaper = GameObject.FindGameObjectWithTag("MailPaper").gameObject.GetComponent<UIMailPaper>();
        string section = govermentStatus.goverment.govermentInSection.ToString();
        float tax = playerStatus.PayTaxes();
        float newCash = playerStatus.player_accounts.getPocket()[section] - tax;
        float newCashGoverment = govermentStatus.goverment.govermentPockets.getPocket()[section] + tax;
        int[] date = time_system.getDateTime();
        if (newCash < 0)
        {
            Debug.Log("No Enough money");
            uiMailPaper.SetAlert("เงินไม่เพียงพอ");
        } else
        {
            Debug.Log("Finish Tax");
            AccountsDetail player_account = new AccountsDetail() { 
                date = date, 
                accounts_name = $"จ่ายภาษี {section}", 
                account_type= "expenses", income = 0, 
                expense = tax 
            }; 
            AccountsDetail Goverment_account = new AccountsDetail()
            {
                date = date,
                accounts_name = $"{playerStatus.getPlayerName()} จ่ายภาษี",
                account_type = "income",
                income = tax,
                expense = 0
            };

            playerStatus.addAccountsDetails(player_account);
            playerStatus.player_accounts.setPocket(section, newCash);

            govermentStatus.addAccountsDetail(Goverment_account);
            govermentStatus.goverment.govermentPockets.setPocket(section, newCashGoverment);

            uiMailPaper.SetAlert("จ่ายภาษีเรียบร้อยแล้ว");
            uiMailPaper.SetCorrectBtn(false);
        }


    }

}
