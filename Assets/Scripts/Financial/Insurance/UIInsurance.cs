using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInsurance : MonoBehaviour
{
    public Insurance_manager insurance_manager;
    [SerializeField]
    private GameObject insuranceCardTemplate;

    [SerializeField]
    private Transform endowment_table;
    [SerializeField]
    private Transform Hearth_table;
    [SerializeField]
    private GameObject InsurancePaper;
    GameObject endowmentCard;
    GameObject HearthCard;

    Button endowmentBtn;
    Button HearthBtn;
    public void UpdateUIInsurance()
    {
        List<InsuranceItems> endowments = insurance_manager.GetEndowments();
        foreach(InsuranceItems insurance in endowments)
        {
            endowmentCard = Instantiate(insuranceCardTemplate, endowment_table);
            endowmentCard.gameObject.GetComponent<UIInsuraceCard>().SetData(insurance.insurance.logo,insurance.insurance.insurance_name);
            endowmentCard.gameObject.GetComponent<UIInsuraceCard>().SetInsurance(insurance);
            endowmentBtn = endowmentCard.gameObject.GetComponent<Button>();
            endowmentBtn.AddEventListener(insurance, OnClickEndowmentAction);
        }

        List<InsuranceItems> healthInsurances = insurance_manager.GetHealthInsurance();
        foreach(InsuranceItems insurance in healthInsurances)
        {
            HearthCard = Instantiate(insuranceCardTemplate, Hearth_table);
            HearthCard.gameObject.GetComponent<UIInsuraceCard>().SetData(insurance.insurance.logo, insurance.insurance.insurance_name);
            HearthCard.gameObject.GetComponent<UIInsuraceCard>().SetInsurance(insurance);
            HearthBtn = HearthCard.gameObject.GetComponent<Button>();
            HearthBtn.AddEventListener(insurance, OnClickHearthAction);
        }
    }

    void OnClickEndowmentAction(InsuranceItems insurance)
    {
        string name = insurance.insurance.insurance_name;
        string description = "คุ้มครองสินค้า" + insurance.insurance.insurance_percent + " % มูลค่าไม่เกิน " + insurance.insurance.insurance_limit.ToString("F") + " <sprite index=0>";
        List<float> amounts = new List<float>();
        List<int> years = new List<int>();

        foreach(Suppackage suppackage in insurance.subpackage){
            Debug.Log("Price: " + suppackage.price);
            amounts.Add(suppackage.price);
            years.Add(suppackage.year);
        }

        InsurancePaper.gameObject.SetActive(true);
        InsurancePaper.gameObject.GetComponent<UIInsurancePaper>().SetData(insurance,name, description, amounts, years);
    }

    void OnClickHearthAction(InsuranceItems insurance)
    {
        string name = insurance.insurance.insurance_name;
        string description = "คุ้มครองราคา " + insurance.insurance.insurance_percent + " % มูลค่าไม่เกิน " + insurance.insurance.insurance_limit.ToString("F") + " <sprite index=0>";
        List<float> amounts = new List<float>();
        List<int> years = new List<int>();

        foreach (Suppackage suppackage in insurance.subpackage)
        {
            amounts.Add(suppackage.price);
            years.Add(suppackage.year);
        }

        InsurancePaper.gameObject.SetActive(true);
        InsurancePaper.gameObject.GetComponent<UIInsurancePaper>().SetData(insurance,name, description, amounts, years);
    }

    public void Close()
    {
        insurance_manager.Banker.gameObject.GetComponent<NPCController>().playerIsClose = true;
        int len_endowment_table = endowment_table.childCount;
        int len_Hearth_table = Hearth_table.childCount;
        for (int i = 0; i < len_endowment_table; i++)
        {
            Destroy(endowment_table.GetChild(i).gameObject);
        }

        for (int i = 0; i < len_Hearth_table; i++)
        {
            Destroy(Hearth_table.GetChild(i).gameObject);
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
