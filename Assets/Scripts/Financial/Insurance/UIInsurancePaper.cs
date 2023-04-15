using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInsurancePaper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Insurance_name;
    [SerializeField]
    private TextMeshProUGUI Insurance_description;
    [SerializeField]
    private TextMeshProUGUI Insurance_type1;
    [SerializeField]
    private TextMeshProUGUI Insurance_type2;
    [SerializeField]
    private TextMeshProUGUI Insurance_type3;

    [SerializeField]
    private Dropdown dropType;
    [SerializeField]
    private Button BuyBtn;

    private PlayerStatus player_status;
    private InsuranceController player_Insurance;
    private SceneStatus scene;
    private List<float> amounts;
    private List<int> years;
    private Timesystem time_system;
    float amount;
    int year;
    public void SetData(InsuranceItems insurance, string name, string description, List<float> amount, List<int> year)
    {
        amounts = amount;
        years = year;
        this.Insurance_name.text = name;
        this.Insurance_description.text = description;
        this.Insurance_type1.text = amount[0].ToString("F") + " <sprite index=0>" + " ราคาประกัน " + year[0].ToString() + " ปี (type1)";
        this.Insurance_type2.text = amount[1].ToString("F") + " <sprite index=0>" + " ราคาประกัน " + year[1].ToString() + " ปี (type2)";
        this.Insurance_type3.text = amount[2].ToString("F") + " <sprite index=0>" + " ราคาประกัน " + year[2].ToString() + " ปี (type3)";
        this.BuyBtn.AddEventListener(insurance, OnBuyInsuranceAction);
    }

    void OnBuyInsuranceAction(InsuranceItems insurance)
    {
        string type = insurance.insurance.insurance.ToString();

        InsuranceItems newinsurance = insurance;
        int[] newBuyDay = time_system.getDateTime();

        int[] newExpireDay = time_system.getDateTime();
        newExpireDay[2] += insurance.subpackage[dropType.value].year;

        newinsurance = newinsurance.ChangeBuyDay(newBuyDay);
        newinsurance = newinsurance.ChangeExpireDay(newExpireDay);

        if (type == "endowment")
        {

            player_Insurance.SetPlayer_endowment(newinsurance, insurance.subpackage[dropType.value].price);

            AccountsDetail newAccount = new AccountsDetail() { date = time_system.getDateTime(), accounts_name = $"buy {type}", account_type = "IEE", income = 0, expense = insurance.subpackage[dropType.value].price, currencyIncome_Type = scene.sceneInsection, currencyExpense_Type = scene.sceneInsection };

            player_status.addAccountsDetails(newAccount);
        } 
        else if(type == "health_insurance")
        {
            player_Insurance.SetPlayer_health_insurance(newinsurance, insurance.subpackage[dropType.value].price);

            AccountsDetail newAccount = new AccountsDetail() { date = time_system.getDateTime(), accounts_name = $"buy {type}", account_type = "IHE", income = 0, expense = insurance.subpackage[dropType.value].price, currencyIncome_Type = scene.sceneInsection, currencyExpense_Type = scene.sceneInsection };

            player_status.addAccountsDetails(newAccount);
        }

        float newPrice = player_status.player_accounts.getPocket()[scene.sceneInsection.ToString()] - insurance.subpackage[dropType.value].price;
        player_status.player_accounts.setPocket(scene.sceneInsection.ToString(), newPrice);

        
    }

    private void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();        
        player_Insurance = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        scene = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
    }

    private void Update()
    {
        float amount = amounts[dropType.value];
        int year = years[dropType.value];
        if (amount <= player_status.player_accounts.getPocket()[scene.sceneInsection.ToString()])
        {
            BuyBtn.interactable = true;
        }
        else
        {
            BuyBtn.interactable = false;
        }
    }


}
