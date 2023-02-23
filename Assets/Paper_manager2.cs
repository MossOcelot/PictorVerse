using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper_manager2 : MonoBehaviour
{
    public StockSystem stock;

    public GameObject priceInput;
    public GameObject quantityInput;
    public Text balanceText;
    public Text valueItem;
    public Text withdrawalText;
    public float price = -1;
    public float quantity = -1;

    [SerializeField]
    float balance;
    [SerializeField]
    float totalValueItems;
    [SerializeField]
    float withdrawal_value;

    private void Update()
    {
        balance = stock.getBalance();
        balanceText.text = balance.ToString("F");
    }

    public void setValueItems()
    {

        if (float.TryParse(priceInput.gameObject.GetComponent<InputField>().text, out price) && float.TryParse(quantityInput.gameObject.GetComponent<InputField>().text, out quantity))
        {
            totalValueItems = price * quantity;
            withdrawal_value = balance + totalValueItems;
            valueItem.text = "$ " + totalValueItems.ToString("F");
            withdrawalText.text = "$ " + withdrawal_value.ToString("F");
        }
    }

    public void OnClickSell()
    {
        
    }
}
