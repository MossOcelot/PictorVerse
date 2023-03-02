using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StockSystem : MonoBehaviour
{
    

    public List<ItemInStock> stock;
    public string market_name;
    public int marketID => GetInstanceID();
    public int VAT;
    [SerializeField]
    private float balance;
    public Text vat_text;
    public OrderOperation player;

    public float getBalance() { return balance; }
    public void setBalance(float balance) { this.balance = balance;}

    private void Start()
    {
        VAT = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>().getVat();
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<OrderOperation>();
        vat_text.text = VAT.ToString() + " %";
    }
    private void Update()
    {
        foreach(ItemInStock itemInStock in stock)
        {
            foreach(ItemStock item in itemInStock.itemStock) 
            {
                int len_OrderList = item.orderBook.getSellOrders().Count;
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
