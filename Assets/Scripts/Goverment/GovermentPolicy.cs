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
    [SerializeField]
    private GovermentStatus govermentStatus;

    [SerializeField]
    private int vat_tax;
    [SerializeField]
    private int business_tax;
    [SerializeField]
    private List<IndividualRangeTax> individual_tax;
    [SerializeField]
    private int travel_tax;
    [SerializeField]
    private int vehicle_tax;
    [SerializeField]
    private int personal_star_tax;

    [SerializeField]
    private int[] taxCollectionDay;

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
                    Mail newMail = new Mail("Tax", $"ใบแจ้งเสียภาษี วันที่ {date[0]}/{date[1]}/{date[2]}",
                        $"{player.getPlayerName()} ต้องเสียภาษีเป็นจำนวน {player.PayTaxes()}", ()=> TaxReport(player));
                    mail_manager.AddMails(newMail);
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
                accounts_name = $"จ่ายภาษีของ {section}", 
                account_type= "expenses", income = 0, 
                expense = tax 
            }; 
            AccountsDetail Goverment_account = new AccountsDetail()
            {
                date = date,
                accounts_name = $"{playerStatus.getPlayerName()} ได้เสียภาษี",
                account_type = "income",
                income = tax,
                expense = 0
            };

            playerStatus.addAccountsDetails(player_account);
            playerStatus.player_accounts.setPocket(section, newCash);

            govermentStatus.addAccountsDetail(Goverment_account);
            govermentStatus.govermentPockets.setPocket(section, newCashGoverment);

            uiMailPaper.SetAlert("จ่ายเรียบร้อย");
            uiMailPaper.SetCorrectBtn(false);
        }


    }

}
