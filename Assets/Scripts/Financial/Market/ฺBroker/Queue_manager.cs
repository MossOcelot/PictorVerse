using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue_manager : MonoBehaviour
{
    public data_storage Storage;
    public void addQueueOrder(OrderOperation customer, string broker_name, LimitOrder limitOrder, int status)
    {
        BrokerOrder newBrokerOrder = new BrokerOrder(customer, broker_name, limitOrder, status, false);
        Storage.addListBrokerOrder(newBrokerOrder);
    }
}
