using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowDataUpdate : MonoBehaviour
{
    [SerializeField]
    private Text gold_text;
    [SerializeField]
    private Text money_text;
    [SerializeField]
    private Text value_item_text;
    [SerializeField]
    private Text product_insurance_text;

    private InsuranceController player_insurance;
    private SceneStatus myScene;
    private PlayerStatus player_status;
    private AllItemInMarket alliteminMarket;
    [SerializeField]
    private InventorySO playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        myScene = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        player_insurance = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        alliteminMarket = GameObject.FindGameObjectWithTag("AllMarket").gameObject.GetComponent<AllItemInMarket>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float gold = player_status.player_accounts.getPocket()["gold"];
        float moneyInSection = player_status.player_accounts.getPocket()[myScene.sceneInsection.ToString()];
        float total_value = GetValueItem();

        gold_text.text = gold.ToString("0.0000");
        money_text.text = moneyInSection.ToString("F");
        value_item_text.text = total_value.ToString("F");

        InsuranceSO insurance = player_insurance.GetPlayer_endowment().insurance;
        if (insurance != null)
        {
            product_insurance_text.text = insurance.insurance_limit.ToString("F");
        }
        
    }

    public float GetValueItem()
    {
        Dictionary<int, InventoryItem> items = playerInventory.GetCurrentInventoryState();
        float totalValue = 0f;
        foreach(InventoryItem item in items.Values)
        {
            Dictionary<int, float> allItems = alliteminMarket.GetitemsInMarket();
            float item_price = 0f;
            if (allItems.ContainsKey(item.item.item_id))
            {
                item_price = allItems[item.item.item_id];
            }
            
            totalValue += (item_price * item.quantity);
        }
        return totalValue;
    }
}
