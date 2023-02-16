using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StockSystem : MonoBehaviour
{
    [System.Serializable]
    public class ItemInStock
    {
        public string item_categories;
        public List<ItemStock> itemStock;
    }

    public List<ItemInStock> stock;
    public int VAT;
    [SerializeField]
    private float balance;

    public float getBalance() { return balance; }
    public void setBalance(float balance) { this.balance = balance;}

    private void Start()
    {
        VAT = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>().getVat();
    }
    private void Update()
    {
        foreach(ItemInStock itemInStock in stock)
        {
            foreach(ItemStock item in itemInStock.itemStock) 
            {
                int len_OrderList = item.orderBook.getSellOrders().Count;
                Debug.Log(item.item.item_id);
                if (len_OrderList > 0) 
                {
                    
                    foreach(LimitOrder order in item.orderBook.getSellOrders().ToList())
                    {

                        dynamic[] q = item.orderBook.MatchOrder(order);
                        
                        order.Quantity -= q[0];
                        order.GainQuantity += q[0];
                        order.GainPrice += q[1];
                        order.status = 2;

                        if (order.Quantity == 0)
                        {
                            order.status = 3;
                            item.orderBook.removeSellOrder(order);
                        }
                    }
                }
            }
        }
    }
}
