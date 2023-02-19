using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broker_manager1 : MonoBehaviour
{
    public data_storage storage;

    private void Update()
    {
        List<BrokerOrder> orders = storage.getListBrokerOrder();
        int len = orders.Count;
        // send order to market
        for(int i = 0; i < len; i++)
        {
            BrokerOrder order = orders[i];
            if (!order.IsActive)
            {
                string marketTarget = order.Order.OrderId.Substring(0, 2);
                if (marketTarget == "m1")
                {
                    StockSystem stockSystem = GameObject.FindGameObjectWithTag("m1").gameObject.transform.GetChild(0).gameObject.GetComponent<StockSystem>();
                    foreach(ItemInStock item_categories in stockSystem.stock)
                    {
                        bool isStop = false;
                        foreach (ItemStock item in item_categories.itemStock)
                        {
                            int itemIdInOrder = int.Parse(order.Order.OrderId.Substring(2, 5));
                            if (item.item.item_id == itemIdInOrder)
                            {
                                order.Order.status = 1;
                                item.orderBook.AddOrder(order.Order);
                                item.QuantityOrder += 1;
                                storage.updateActivateBrokerOrder(i ,true);
                                isStop = true;
                                break;
                            }

                        }
                        if (isStop)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
