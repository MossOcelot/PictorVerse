using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GovermentPolicy : MonoBehaviour
{
    public GovermentPolicyData govermentPolicy;
    public GovermentStatus govermentStatus;
    public GameObject FileTax_Template;
    public Transform content_FileTax;
    
    Timesystem time_system;

    public bool GovermentAlert;
    public bool IsSent;

    public int NonPayTaxDay;

    int oldDate;
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
        Debug.Log("FixedUpdate");
        int[] date = time_system.getDateTime();
        if (date[2] == govermentPolicy.taxCollectionDay[2])
        {
            if (date[1] == govermentPolicy.taxCollectionDay[1])
            {
                if (date[0] == govermentPolicy.taxCollectionDay[0])
                {
                    if (IsSent) return;
                    UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
                    mail_manager.AddMail("Goverment", $"ถึงเวลาจ่ายภาษีแล้ว ของปี {date[2] - 1}",
                        $"โปรดยื่นภาษีและจ่ายภาษีให้เรียบร้อยภายใน 30 วัน นับจากวันที่ {date[0]}/{date[1]}/{date[2]}", null, null);
                    GameObject FileTax = Instantiate(FileTax_Template, content_FileTax);
                    govermentPolicy.taxCollectionDay[2]++;
                    GovermentAlert = true;
                    IsSent = true;
                } else
                {
                    IsSent = false;
                }
            }
        }

        if(GovermentAlert && (date[0] != oldDate)) 
        {
            if(NonPayTaxDay >= 30)
            {
                LoanPlayerController loanPlayer = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<LoanPlayerController>();
                PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
                float incomeAll = playerStatus.GetIncomeTypeInYear("FI") + playerStatus.GetIncomeTypeInYear("MI") + playerStatus.GetIncomeTypeInYear("RI");
                float tax = playerStatus.PayTaxes(incomeAll);
                float newDebt = loanPlayer.GetDept() + (tax * 2);
                loanPlayer.SetDebt(newDebt);


                UIMailBox mail_manager = GameObject.FindGameObjectWithTag("MailBox").gameObject.GetComponent<UIMailBox>();
                mail_manager.AddMail("Goverment", $"โดนปรับภาษี",
                    $"เนื่องจากท่านไม่ได้เสียภาษีตามเวลาที่กำหนด ท่านได้ถูกปรับเป็นจำนวน {tax * 2} ซึ่งเป็น 2 เท่าของภาษีที่ทั่นต้องเสีย โปรดที่รายการบัญชี ค่าปรับจะถูกบวกเข้ากับหนี้สินของท่าน", null, null);
            }
            NonPayTaxDay += 1;
            oldDate = date[0];
        }
    }

}
