using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper_Manager : MonoBehaviour
{
    public StockSystem stock;

    public GameObject priceInput;
    public GameObject quantityInput;
    public Text balanceText;
    public Text valueItem;
    public Text Vat_Value;
    public Text totalText;
    public Text withdrawalText;
    float price = -1;
    float quantity = -1;

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
