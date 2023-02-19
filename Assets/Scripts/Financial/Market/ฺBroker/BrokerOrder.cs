using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrokerOrder
{
    public OrderOperation Customer;
    public string Broker;
    public LimitOrder Order;
    public bool IsActive;

    public BrokerOrder(OrderOperation customer, string broker, LimitOrder limitOrder, int status, bool isActive)
    {
        Customer = customer;
        Broker = broker;
        Order = limitOrder;
        IsActive = isActive;
    }

}
