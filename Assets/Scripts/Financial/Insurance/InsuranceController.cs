using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class InsuranceController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player_status;
    [SerializeField]
    private InsuranceItems player_endowment;
    [SerializeField]
    private InsuranceItems player_hearth_insurance;

    public List<InventorySO> playerInventoryList;
    private Timesystem time_system;
    private AllItemInMarket allItemInMarket;

    public float credit_endowment;
    public float credit_hearth;

    public List<InsuranceData> insuranceDataBuy;
    public InsuranceItems GetPlayer_endowment()
    {
        return this.player_endowment;
    }
    public void SetPlayer_endowment(InsuranceItems insurance, float amount)
    {
        this.player_endowment = insurance;
        AddInsuranceData(insurance.buyDay, "Endowment", amount);
        SetCreditHealth(insurance);
    }

    public InsuranceItems GetPlayer_hearth_insurance()
    {
        return this.player_hearth_insurance;
    }

    public void SetPlayer_health_insurance(InsuranceItems insurance, float amount)
    {
        this.player_hearth_insurance = insurance;
        AddInsuranceData(insurance.buyDay, "Health_insurance", amount);
        SetCreditEndowment(insurance);
    }

    private void Awake()
    {
        // Load();
    }

    private void Start()
    {
        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        allItemInMarket = GameObject.FindGameObjectWithTag("AllMarket").gameObject.GetComponent<AllItemInMarket>();
    }

    private void Update()
    {
        CheckInsuranceExpireDay();
    }

    public bool DeadClearItem()
    {
        int totalQuantity = 0;
        foreach (InventorySO inventory in playerInventoryList)
        {
            Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
            foreach (int index in items.Keys)
            {
                totalQuantity += items[index].quantity;
            }
        }
        if (totalQuantity == 0) return false;

        bool IsSuccess = endowment_claim();
        if (IsSuccess)
        {
            player_endowment = new InsuranceItems();
            RemoveCreditEndowment();
        }
        else
        {
            // drop all item
            foreach (InventorySO inventory in playerInventoryList)
            {
                Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
                foreach (int index in items.Keys)
                {
                    inventory.RemoveItem(index, items[index].quantity);
                }
            }
        }
        player_status.IsDead = false;
        return true;
    }

    private void CheckInsuranceExpireDay()
    {
        int[] time = time_system.getDateTime();
        int[] endowmentDay = player_endowment.expireDay;
        int[] healthDay = player_hearth_insurance.expireDay;

        if (player_endowment.insurance != null)
        {
            if (time[2] == endowmentDay[2] && time[1] == endowmentDay[1] && time[0] == endowmentDay[0])
            {
                // ใส่แจ้งเตือนทางจดหมาย 
                Debug.Log("Endowment has expired.");

                player_endowment = new InsuranceItems();
                RemoveCreditEndowment();
            }
        }

        if (player_hearth_insurance.insurance != null)
        {
            if (time[2] == healthDay[2] && time[1] == healthDay[1] && time[0] == healthDay[0])
            {
                // ใส่แจ้งเตือนทางจดหมาย 
                Debug.Log("HealthInsurance has expired.");

                player_hearth_insurance = new InsuranceItems();
                RemoveCreditHealth();
            }
        }
    }

    public bool endowment_claim()
    {
        InsuranceSO insurance = player_endowment.insurance;
        if (insurance != null) 
        {
            int totalQuantity = 0;
            foreach (InventorySO inventory in playerInventoryList)
            {
                Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
                foreach (int index in items.Keys)
                {
                    totalQuantity += items[index].quantity;
                }
            }

            int quantityLimit = Mathf.RoundToInt((float)totalQuantity * (insurance.insurance_percent / 100));
            float priceLimit = insurance.insurance_limit;
            int quantityItems = 0;
            float priceItems = 0f;
            int n = 0;
            foreach (InventorySO inventory in playerInventoryList)
            {
                Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
                foreach (int index in items.Keys)
                {
                    quantityItems += items[index].quantity;
                    priceItems += (items[index].quantity * allItemInMarket.GetitemsInMarket()[items[index].item].price);

                    if (quantityItems > quantityLimit || priceItems > priceLimit)
                    {
                        Debug.Log("item quantity: " + items[index].quantity + " quantityItems:" + quantityItems + " quantityLimit: " + quantityLimit + " priceLimit: " + priceLimit + " priceItems: " + priceItems);
                        if(n == 0)
                        {
                            if(quantityItems > quantityLimit)
                            {
                                inventory.RemoveItem(index, quantityItems - quantityLimit);
                            } else if(priceItems > priceLimit)
                            {
                                float diffPrice = priceItems - priceLimit;
                                int quantity = Mathf.FloorToInt(diffPrice / allItemInMarket.GetitemsInMarket()[items[index].item].quantity);
                                inventory.RemoveItem(index, quantity);
                            }
                            n++;
                            continue;
                        }
                        
                        inventory.RemoveItem(index, items[index].quantity);
                    }


                }
            }

            return true; 
        }
        return false;
    }

    public float hearth_insurance_claim(float health_care_price, string section_name, string command)
    {
        InsuranceSO insurance = player_hearth_insurance.insurance;
        if(insurance != null)
        {
            float full_protect_price = health_care_price * (insurance.insurance_percent / 100f);

            float protect_price = 0f;
            if (full_protect_price > insurance.insurance_limit)
            {
                protect_price = insurance.insurance_limit;
            }
            else
            {
                protect_price = full_protect_price;
            }

            // add update value money insurance 
            // 

            if (command != "Get")
            {
                Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();

                float newValue = player_status.player_accounts.getPocket()[section_name] + protect_price;
                player_status.player_accounts.setPocket(section_name, newValue);

                int[] dateTime = date.getDateTime();
                AccountsDetail account = new AccountsDetail() { date = dateTime, accounts_name = "hearth_insurance_claim", account_type = "claim_insurance", income = protect_price, expense = 0 };

                Debug.Log("Cost Cost: " + protect_price);
                player_status.addAccountsDetails(account);
            } 

            Debug.Log("protect_price sent to player success.");
            return protect_price;
        }

        return -1;
    }

    private void SetCreditEndowment(InsuranceItems insurance)
    {
        if(insurance.insurance != null)
        {
            credit_endowment = insurance.insurance.insurance_limit / 10000f;
            float newCredit = player_status.getMyStatic().static_credibility + credit_endowment;
            player_status.setMyStatic(7, newCredit);
            return;
        }
        RemoveCreditEndowment();
    }

    private void RemoveCreditEndowment()
    {
        float new_credit = player_status.getMyStatic().static_credibility - credit_endowment;
        credit_endowment = 0;
        player_status.setMyStatic(7, new_credit);
    }

    private void SetCreditHealth(InsuranceItems insurance)
    {
        if (insurance.insurance != null)
        {
            credit_hearth = insurance.insurance.insurance_limit / 100000f;
            float newCredit = player_status.getMyStatic().static_credibility + credit_hearth;
            player_status.setMyStatic(7, newCredit);
            return;
        }
        RemoveCreditHealth();
    }

    private void RemoveCreditHealth()
    {
        float new_credit = player_status.getMyStatic().static_credibility - credit_hearth;
        credit_hearth = 0;
        player_status.setMyStatic(7, new_credit);
    }

    private void AddInsuranceData(int[] date, string type, float amount)
    {
        InsuranceData newData = new InsuranceData(date, type, amount);
        insuranceDataBuy.Insert(0, newData);
    }

    public float GetInsuranceCostInYear(string type)
    {
        float allYearCost = 0;

        GovermentPolicy goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();
        int[] date = goverment.govermentPolicy.taxCollectionDay;

        foreach (InsuranceData data in insuranceDataBuy)
        {
            int[] account_date = data.buyDate;
            if (account_date[2] >= date[2] - 1)
            {
                if (account_date[1] == date[1])
                {
                    if (account_date[0] >= date[0])
                    {
                        break;
                    }
                }
                else if (account_date[1] < date[1])
                {
                    break;
                }
            }

            if (data.type_insurance == type)
            {
                allYearCost += data.amount;
            }
        }

        return allYearCost;
    }
    private void OnApplicationQuit()
    {
        // Save();
    }
    // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerInsurance(this);
    }

    public void Load()
    {
        PlayerInsuranceData data = SavePlayerSystem.LoadPlayerInsurance();

        if (data != null)
        {
            player_endowment = data.player_endowment;
            player_hearth_insurance = data.player_hearth_insurance;
        }
    }



}

[System.Serializable]
public class InsuranceData
{
    public int[] buyDate;
    public string type_insurance;
    public float amount;

    public InsuranceData(int[] buyDate, string type, float cost)
    {
        this.buyDate = buyDate;
        type_insurance = type;
        amount = cost;
    }
}