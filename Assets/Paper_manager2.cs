using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Paper_manager2 : MonoBehaviour
{
    public StockSystem stock;
    public Page_Manager page_manager;
    public Orderbook_manager orderbook;

    public GameObject priceInput;
    public GameObject quantityInput;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI valueItem;
    public TextMeshProUGUI withdrawalText;
    public TextMeshProUGUI QuantityItem;
    public float price = -1;
    public float quantity = -1;
    public Button SellBtn;

    [SerializeField]
    float balance;
    [SerializeField]
    float quantityItem;
    [SerializeField]
    float totalValueItems;
    [SerializeField]
    float withdrawal_value;
    [SerializeField]
    List<int> itemIndexs;
    private void Update()
    {
        balance = stock.getBalance();
        balanceText.text = balance.ToString("F");

        quantityItem = getQuantityItem();
        QuantityItem.text = quantityItem.ToString();

        Debug.Log(price + " : " + orderbook.values_list[17]);
        if (quantity > quantityItem || price < orderbook.values_list[0] || price > orderbook.values_list[17])
        {
            SellBtn.interactable = false;
        }
        else
        {
            SellBtn.interactable = true;
        }
    }

    private int getQuantityItem()
    {
        itemIndexs = new List<int>();
        InventorySO items = stock.player.playerInventorySO;
        
        int quantity = 0;

        int index = 0;
        foreach (InventoryItem item in items.GetCurrentInventoryState().Values)
        {
            if(item.item.item_id == page_manager.ItemInStock.item.item_id)
            {
                quantity += item.quantity;
                itemIndexs.Add(index);
            }
            index++;
        }

        return quantity;
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
        Queue_manager broker = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(0).gameObject.GetComponent<Queue_manager>();

        LimitOrder limitOrder = new LimitOrder("", stock.player, totalValueItems / quantity, (int)quantity, false);

        Timesystem time = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        PlayerActivityController activity_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
        activity_controller.AddActivity(time.getDateTime(), UIHourActivity.acitivty_type.shopping);

        foreach (int n in itemIndexs)
        {
            Dictionary<int, InventoryItem> PlayerInventory = stock.player.playerInventorySO.GetCurrentInventoryState();
            bool success = false;
            foreach(int i in PlayerInventory.Keys)
            {
                if (n == i)
                {
                    if (PlayerInventory[n].quantity >= (int)quantity)
                    {
                        Debug.Log("Sell Success: " + quantity);
                        stock.player.playerInventorySO.RemoveItem(n, (int)quantity);
                        success = true;
                        break;
                    }
                    stock.player.playerInventorySO.RemoveItem(n, (int)quantity);
                    Debug.Log("balance Quantity: " + (float)PlayerInventory[n].quantity);
                    quantity -= (float)PlayerInventory[n].quantity;
                }
            }
            if (success)
            {
                break;
            }
        }

        priceInput.gameObject.GetComponent<InputField>().text = "0";
        quantityInput.gameObject.GetComponent<InputField>().text = "0";

        broker.addQueueOrder(stock.market_name, page_manager.ItemInStock.item.item_id, "System", limitOrder, 0);
    }
}
