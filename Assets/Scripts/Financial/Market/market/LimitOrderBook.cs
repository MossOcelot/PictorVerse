using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimitOrderBook
{
    public float marketPrice;
    public int quantityItem;
    [SerializeField]
    private List<LimitOrder> buyOrders;
    [SerializeField]
    private List<LimitOrder> sellOrders;

    public void removeSellOrder(LimitOrder order)
    {
        sellOrders.Remove(order);
    }
    public List<LimitOrder> getBuyOrders()
    {
        return buyOrders;   
    }

    public List<LimitOrder> getSellOrders()
    {
        return sellOrders;
    }

    public void UpdateorderInBook(LimitOrder order)
    {
        if (order.IsBuy)
        {
            int len = buyOrders.Count;
            for(int i = 0; i < len; i++) 
            {
                if (buyOrders[i].OrderId == order.OrderId)
                {
                    buyOrders.RemoveAt(i);
                    break;
                }
            }

        }
        else
        {
            int len = sellOrders.Count;
            for (int i = 0; i < len; i++)
            {
                if (sellOrders[i].OrderId == order.OrderId)
                {
                    sellOrders.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void AddOrder(LimitOrder order)
    {
        if (order.IsBuy)
        {
            buyOrders.Add(order);
            buyOrders.Sort((a, b) => -a.Price.CompareTo(b.Price)); // Sort descending by price
        }
        else
        {
            sellOrders.Add(order);
            quantityItem += order.Quantity;
            sellOrders.Sort((a, b) => a.Price.CompareTo(b.Price)); // Sort ascending by price
        }
    }

    public dynamic[] MatchOrder(LimitOrder order)
    {
        bool isBuy = order.IsBuy;
        float price = order.Price;
        int quantity = order.Quantity;

        List<LimitOrder> orders = isBuy ? sellOrders : buyOrders;
        int totalQuantity = 0;
        float gainprice = 0;

        for (int i = 0; i < orders.Count; i++)
        {
            if (order.Customer == orders[i].Customer) continue;
            if ((isBuy && orders[i].Price > price) || (!isBuy && orders[i].Price < price))
            {
                break; // Stop matching orders if price is no longer favorable
            }

            int matchedQuantity = Mathf.Min(quantity, orders[i].Quantity);
            if (matchedQuantity == quantity)
            {
                order.GainPrice += ((float)quantity * price);
            } else
            {
                orders[i].GainPrice += ((float)orders[i].Quantity * orders[i].Price);
            }
            
            orders[i].status = 2;
            orders[i].Quantity -= matchedQuantity;
            orders[i].GainQuantity += matchedQuantity;
            totalQuantity += matchedQuantity;
            quantity -= matchedQuantity;
            quantityItem -= matchedQuantity;

            gainprice += ((float)matchedQuantity * orders[i].Price);
            marketPrice = orders[i].Price;
            if (orders[i].Quantity == 0)
            {
                orders[i].status = 3;
                Debug.Log(orders[i].OrderId);
                orders.RemoveAt(i);
                i--;
            }

            if (quantity == 0)
            {
                break; // Stop matching orders if all quantity is filled
            }
        }

        return new dynamic[] { totalQuantity, gainprice };
    }
}
