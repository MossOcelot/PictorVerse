using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class InsuranceController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player_status;
    [SerializeField]
    private InsuranceItems player_endowment;
    [SerializeField]
    private InsuranceItems player_hearth_insurance;

    [SerializeField]
    private List<InventorySO> playerInventoryList;
    private Timesystem time_system;
    private AllItemInMarket allItemInMarket;

    public InsuranceItems GetPlayer_endowment()
    {
        return this.player_endowment;
    }
    public void SetPlayer_endowment(InsuranceItems insurance)
    {
        this.player_endowment = insurance;
    }

    public InsuranceItems GetPlayer_hearth_insurance()
    {
        return this.player_hearth_insurance;
    }

    public void SetPlayer_health_insurance(InsuranceItems insurance)
    {
        this.player_hearth_insurance = insurance;
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

        if(player_status.IsDead == true)
        {
            Debug.Log("true Dead");
            bool IsSuccess = endowment_claim();
            if(IsSuccess) 
            {
                player_endowment = new InsuranceItems();
            } else
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
        }
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
            }
        }

        if (player_hearth_insurance.insurance != null)
        {
            if (time[2] == healthDay[2] && time[1] == healthDay[1] && time[0] == healthDay[0])
            {
                // ใส่แจ้งเตือนทางจดหมาย 
                Debug.Log("HealthInsurance has expired.");

                player_hearth_insurance = new InsuranceItems();
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
                                Debug.Log("items");
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

    public bool hearth_insurance_claim(float health_care_price, string section_name)
    {
        InsuranceSO insurance = player_hearth_insurance.insurance;
        if(insurance != null)
        {
            float full_protect_price = health_care_price * insurance.insurance_percent;

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

            float newValue = player_status.player_accounts.getPocket()[section_name] + protect_price;
            player_status.player_accounts.setPocket(section_name, newValue);

            Debug.Log("protect_price sent to player success.");
            return true;
        }

        return false;
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
