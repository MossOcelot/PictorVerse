using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper_Manager : MonoBehaviour
{
    public Page_Manager page_manager;
    public StockSystem stock;
    public GameObject priceInput;
    public GameObject quantityInput;
    public Text balanceText;
    public Text valueItem;
    public Text Vat_Value;
    public Text totalText;
    public Text withdrawalText;
    public Button BuyBtn;
    public float price = -1;
    public float quantity = -1;

    [SerializeField]
    float VAT;
    [SerializeField]
    float balance;
    [SerializeField]
    float totalValueItems;
    [SerializeField]
    float vat_price;
    [SerializeField]
    float total_value;
    [SerializeField]
    float withdrawal_value;

    private void Update()
    {
        balance = stock.getBalance();
        balanceText.text = balance.ToString("F");

        // button
        if(balance < total_value || totalValueItems == 0.0f)
        {
            BuyBtn.interactable = false;
        } else
        {
            BuyBtn.interactable = true;
        }
    }

    public void OnClickBuy()
    {
        Queue_manager broker = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(0).gameObject.GetComponent<Queue_manager>();
        float newbalance = stock.getBalance() - total_value;
        stock.setBalance(newbalance);

        LimitOrder limitOrder = new LimitOrder("", stock.player, totalValueItems / quantity, (int)quantity, true);
        // add order to queue_manager (broker)
        priceInput.gameObject.GetComponent<InputField>().text = "0";
        quantityInput.gameObject.GetComponent<InputField>().text = "0";


        broker.addQueueOrder(stock.market_name, page_manager.ItemInStock.item.item_id, "System", limitOrder, 0);
    }

    public void setValueItems()
    {

        VAT = (float)stock.VAT / 100;
        
        if (float.TryParse(priceInput.gameObject.GetComponent<InputField>().text, out price) && float.TryParse(quantityInput.gameObject.GetComponent<InputField>().text, out quantity))
        {
            totalValueItems = price * quantity;
            vat_price = totalValueItems * VAT;
            total_value = totalValueItems + vat_price;
            withdrawal_value = balance - total_value;
            valueItem.text = "$ " + totalValueItems.ToString("F");
            Vat_Value.text = "$ " + vat_price.ToString("F");
            totalText.text = "$ " + total_value.ToString("F");
            withdrawalText.text = "$ " + withdrawal_value.ToString("F");
        }
    }
}
