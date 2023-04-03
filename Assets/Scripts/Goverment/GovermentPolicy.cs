using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovermentPolicy : MonoBehaviour
{
    [System.Serializable]
    public class IndividualRangeTax
    {
        public float minIncome;
        public float maxIncome;
        public float Tax;
    }
    public GovermentStatus govermentStatus;

    public int vat_tax;
    public int business_tax;
    public List<IndividualRangeTax> individual_tax;
    public int travel_tax;
    public int vehicle_tax;
    public int personal_star_tax;

    public int[] taxCollectionDay;

    Timesystem time_system;

    public bool IsSent;
    public int getVat()
    {
        return vat_tax;
    }

    public List<IndividualRangeTax> getIndividualTax()
    {
        return individual_tax;
    }
    // Start is called before the first frame update
    void Awake()
    {
        Dictionary<string, dynamic> vat_check = new VATInGalaxy().checkvat(govermentStatus.govermentInSection.ToString());
        
        vat_tax = vat_check["vat_tax"];
        business_tax = vat_check["business_tax"];
        travel_tax = vat_check["travel_tax"];
        vehicle_tax = vat_check["vehicle_tax"];
        personal_star_tax = vat_check["personal_star_tax"];

        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.AddComponent<Timesystem>();
    }

    private void FixedUpdate()
    {
        int[] date = time_system.getDateTime();
        if (date[2] == taxCollectionDay[2])
        {
            if (date[1] == taxCollectionDay[1])
            {
                if (date[0] == taxCollectionDay[0])
                {
                    if (IsSent) return;
                    MailManager mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<MailManager>();
                    PlayerStatus player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
                    Mail newMail = new Mail("Tax", $"ใบแจ้งภาษี วันที่ {date[0]}/{date[1]}/{date[2]}",
                        $"{player.getPlayerName()} มีรายได้ปีที่ผ่านมาอยู่ที่ {player.GetIncomeAllYear()} ต้องเสียภาษีจำนวน {player.PayTaxes()}", ()=> TaxReport(player));
                    mail_manager.AddMails(newMail);

                    taxCollectionDay[2]++;
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
        string section = govermentStatus.govermentInSection.ToString();
        float tax = playerStatus.PayTaxes();
        float newCash = playerStatus.player_accounts.getPocket()[section] - tax;
        float newCashGoverment = govermentStatus.govermentPockets.getPocket()[section] + tax;
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
            govermentStatus.govermentPockets.setPocket(section, newCashGoverment);

            uiMailPaper.SetAlert("จ่ายภาษีเรียบร้อยแล้ว");
            uiMailPaper.SetCorrectBtn(false);
        }


    }

}
