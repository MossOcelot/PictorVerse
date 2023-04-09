using inventory.Model;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class UIPage2Controller : MonoBehaviour
{
    public InsuranceItems insuranceSO;
    public GameObject InsuranceDetails_Template;
    GameObject InsuranceDetails;

    AllItemInMarket allItemInMarket;
    public List<ItemInsuranceDetails> BeforeitemInsuranceDetails;
    public List<ItemInsuranceDetails> AfteritemInsuranceDetails;
    private void Awake()
    {
        allItemInMarket = GameObject.FindGameObjectWithTag("AllMarket").gameObject.GetComponent<AllItemInMarket>();
    }

    public void SetInsuranceSO(InsuranceItems insurance)
    {
        this.insuranceSO = insurance;
    }

    public void AddAllItems()
    {
        insuranceSO = new InsuranceItems();
        InsuranceController player_insurance = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        List<InventorySO> inventories = player_insurance.playerInventoryList;
        BeforeitemInsuranceDetails = new List<ItemInsuranceDetails>();
        foreach (InventorySO inventory in inventories)
        {
            Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
            foreach (InventoryItem inventory_item in items.Values)
            {
                Item item = inventory_item.item;
                int index = CheckItemInsuranceDeutails(item, BeforeitemInsuranceDetails);

                float price_market = allItemInMarket.GetitemsInMarket()[item].price;
                if (index == -1)
                {
                    ItemInsuranceDetails newItemInsurance = new ItemInsuranceDetails(item, inventory_item.quantity, price_market);
                    BeforeitemInsuranceDetails.Add(newItemInsurance);
                }
                else
                {
                    BeforeitemInsuranceDetails[index].quantity += inventory_item.quantity;
                }
            }
        }
    }

    public void AddAllInsuranceItems()
    {
        InsuranceController player_insurance = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        List<InventorySO> inventories = player_insurance.playerInventoryList;
        AfteritemInsuranceDetails = new List<ItemInsuranceDetails>();
        foreach (InventorySO inventory in inventories)
        {
            Dictionary<int, InventoryItem> items = inventory.GetCurrentInventoryState();
            foreach (InventoryItem inventory_item in items.Values)
            {
                Item item = inventory_item.item;
                int index = CheckItemInsuranceDeutails(item, AfteritemInsuranceDetails);

                float price_market = allItemInMarket.GetitemsInMarket()[item].price;
                if (index == -1)
                {
                    ItemInsuranceDetails newItemInsurance = new ItemInsuranceDetails(item, inventory_item.quantity, price_market);
                    AfteritemInsuranceDetails.Add(newItemInsurance);
                }
                else
                {
                    AfteritemInsuranceDetails[index].quantity += inventory_item.quantity;
                }
            }
        }
    }

    private int CheckItemInsuranceDeutails(Item item, List<ItemInsuranceDetails> itemInsuranceDetails)
    {
        int len = itemInsuranceDetails.Count;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                ItemInsuranceDetails item_insurance = itemInsuranceDetails[i];
                if (item.item_id == item_insurance.item.item_id)
                {
                    return i;
                }
            }
        }

        return -1;
    }
    public void AddInsuranceDetailsObj(bool NothaveItem)
    {
        InsuranceDetails = Instantiate(InsuranceDetails_Template, gameObject.transform);
        InsuranceDetailsControl allItemDetails = InsuranceDetails.GetComponent<InsuranceDetailsControl>();
        InsuranceSO insurance = insuranceSO.insurance;
        allItemDetails.SetallItemDetails(BeforeitemInsuranceDetails);
        
        if(insurance != null)
        {
            allItemDetails.SetTitleInsurance(insurance.logo, insurance.insurance_name, $"รับประกันสิ่งของตก {insurance.insurance_percent}% วงเงิน {insurance.insurance_limit}");
            if(NothaveItem)
            {
                allItemDetails.SetItemNone();
            } else
            {
                allItemDetails.SetallItemInsuranceDetails(AfteritemInsuranceDetails);
            }
        } else
        {
            allItemDetails.SetTitleInsuranceNone();
            if(NothaveItem)
            {
                allItemDetails.SetItemNone();
            }
            else
            {
                allItemDetails.SetDataNoneInsurance();
            }
        }
    }
}
