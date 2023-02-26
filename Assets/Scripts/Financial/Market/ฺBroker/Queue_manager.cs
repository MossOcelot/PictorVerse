using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue_manager : MonoBehaviour
{
    public data_storage Storage;
    public void addQueueOrder(string market_name, int item_id, string broker_name, LimitOrder limitOrder, int status)
    {
        limitOrder.OrderId = generateOrderID(market_name, item_id);
        BrokerOrder newBrokerOrder = new BrokerOrder(broker_name, limitOrder, status, false);
        Storage.addListBrokerOrder(newBrokerOrder);
    }

    private string generateOrderID(string market_name, int item_id)
    {
        int queueIndex = QueueOrder(market_name, item_id) + 1;
        return market_name + item_id.ToString() + queueIndex.ToString("0000");
    }

    private int QueueOrder(string market_name, int item_id)
    {
        StockSystem stock = GameObject.FindGameObjectWithTag(market_name).gameObject.transform.GetChild(0).gameObject.GetComponent<StockSystem>();
        foreach (ItemInStock itemInStock in stock.stock)
        {
            foreach(ItemStock itemStock in itemInStock.itemStock)
            {
                if(itemStock.item.item_id == item_id)
                {
                    return itemStock.QuantityOrder;
                }
            }
        }

        return 0;
    }
}
