using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrokerOrder
{
    public string Broker;
    public LimitOrder Order;
    public bool IsActive;

    public BrokerOrder(string broker, LimitOrder limitOrder, int status, bool isActive)
    {
        Broker = broker;
        Order = limitOrder;
        IsActive = isActive;
    }

}
