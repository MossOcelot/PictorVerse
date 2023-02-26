using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderOperation : MonoBehaviour
{
    public PlayerStatus player;
    public InventorySO playerInventorySO;
    public string type;
    public NPC_Shop npc;
    public Queue_manager broker;

    private void Start()
    {
        broker = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(0).gameObject.GetComponent<Queue_manager>();
    }
    public void setStatus(string type, string market, int itemId,  float price, int quantity, bool isBuy)
    {
        string OrderId = generateOrderId(market, itemId);
        if (type == "player")
        {
            OrderOperation myorder = gameObject.GetComponent<OrderOperation>();
            LimitOrder order = new LimitOrder(OrderId, myorder, price, quantity, isBuy);
            broker.addQueueOrder(market, itemId, "system", order, 0);
        }
        else if (type == "npc")
        {
            OrderOperation myorder = gameObject.GetComponent<OrderOperation>();
            LimitOrder order = new LimitOrder(OrderId, myorder, price, quantity, isBuy);
            broker.addQueueOrder(market, itemId, "system", order, 0);
        }
    }

    
    public string generateOrderId(string market, int itemId)
    {
        string orderId = market + itemId.ToString();
        StockSystem stock = GameObject.FindGameObjectWithTag(market).gameObject.transform.GetChild(0).gameObject.GetComponent<StockSystem>();
        foreach (ItemInStock itemInStock in stock.stock)
        {
            bool isfind = false;
            foreach (ItemStock item in itemInStock.itemStock)
            {
                if (item.item.item_id == itemId)
                {
                    orderId += (item.QuantityOrder + 1).ToString();
                    isfind = true;
                    break;
                }
            }
            if (isfind)
            {
                break;
            }
        }
        return orderId;
    }
}
