using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInsurance : MonoBehaviour
{
    [SerializeField] 
    private Insurance_manager insurance_manager;
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
            endowmentCard.gameObject.GetComponent<UIInsuraceCard>().SetData(insurance.insurance.insurance_name);
            endowmentCard.gameObject.GetComponent<UIInsuraceCard>().SetInsurance(insurance);
            endowmentBtn = endowmentCard.gameObject.GetComponent<Button>();
            endowmentBtn.AddEventListener(insurance, OnClickEndowmentAction);
        }

        List<InsuranceItems> healthInsurances = insurance_manager.GetHealthInsurance();
        foreach(InsuranceItems insurance in healthInsurances)
        {
            HearthCard = Instantiate(insuranceCardTemplate, Hearth_table);
            HearthCard.gameObject.GetComponent<UIInsuraceCard>().SetData(insurance.insurance.insurance_name);
            HearthCard.gameObject.GetComponent<UIInsuraceCard>().SetInsurance(insurance);
            HearthBtn = HearthCard.gameObject.GetComponent<Button>();
            HearthBtn.AddEventListener(insurance, OnClickHearthAction);
        }
    }

    void OnClickEndowmentAction(InsuranceItems insurance)
    {
        string name = insurance.insurance.insurance_name;
        string description = "รับประกันของตก " + insurance.insurance.insurance_percent + " % วงเงิน " + insurance.insurance.insurance_limit.ToString("F") + " $";
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
        string description = "คุ้มครองค่ารักษา " + insurance.insurance.insurance_percent + " % วงเงิน " + insurance.insurance.insurance_limit.ToString("F") + " $";
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
