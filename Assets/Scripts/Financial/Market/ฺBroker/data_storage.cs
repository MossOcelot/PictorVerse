using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data_storage : MonoBehaviour
{
    [SerializeField]
    private List<BrokerOrder> brokerOrders;

    public void addListBrokerOrder(BrokerOrder brokerOrder)
    {
        brokerOrders.Insert(0, brokerOrder);
    }

    public void removeBrokerOrder(int index)
    {
        brokerOrders.RemoveAt(index);
    }

    public void updateActivateBrokerOrder(int index, bool value)
    {
        brokerOrders[index].IsActive = value;
    }

    public List<BrokerOrder> getListBrokerOrder()
    {
        return brokerOrders;
    }

}
