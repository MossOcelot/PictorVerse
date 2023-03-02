using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOrderShow : MonoBehaviour
{
    [System.Serializable]
    public class BrokerOrderAndIndex
    {
        public int index;
        public BrokerOrder order;   

        public BrokerOrderAndIndex(int index, BrokerOrder order)
        {
            this.index = index;
            this.order = order;
        }
    }
    public StockSystem stock;
    [SerializeField]
    private OrderOperation player;
    public List<BrokerOrderAndIndex> myOrders;
    public GameObject Table;
    public GameObject ItemOrderbar;

    data_storage OrderStorage;
    GameObject Item;
    Button OrderItemBtn;


    private void Start()
    {
        OrderStorage = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(1).gameObject.GetComponent<data_storage>();
    }


    void getMyOrder()
    {
        List<BrokerOrder> storage = OrderStorage.getListBrokerOrder();
        int n = 0;
        foreach (BrokerOrder myOrder in storage) 
        {

            if (myOrder.Order.Customer.type != "player") continue;
            if(myOrder.Order.Customer.player.player_id == player.player.player_id)
            {
                BrokerOrderAndIndex order = new BrokerOrderAndIndex(n, myOrder);
                myOrders.Add(order);
            }
            n++;
        }
    }
    public void ShowPlayerOrder()
    {
        // reset data
        myOrders = new List<BrokerOrderAndIndex>();

        int len_myOrder = Table.transform.childCount;
        if (len_myOrder != 0)
        {
            for(int i = 0; i < len_myOrder; i++)
            {
                Destroy(Table.transform.GetChild(i).gameObject);
            }
        }

        player = stock.player;
        getMyOrder();
        
        int len = myOrders.Count;
        for (int i = 0; i < len; i++)
        {
            Debug.Log("ii: " + i);
            Item itemSO = new Item();
            foreach (ItemInStock itemInStock in stock.stock)
            {
                foreach(ItemStock item in itemInStock.itemStock)
                {
                    bool isfind = false;
                    if (int.Parse(myOrders[i].order.Order.OrderId.Substring(2, 5)) == item.item.item_id)
                    {
                        itemSO = item.item;
                        isfind = true;
                        break;
                    }
                    if(isfind)
                    {
                        break;
                    }
                }
            }

            Debug.Log(itemSO.item_name);
            float price_itemAVG = myOrders[i].order.Order.GainQuantity * myOrders[i].order.Order.Price;
            float offer_price = (myOrders[i].order.Order.Quantity + myOrders[i].order.Order.GainQuantity) * myOrders[i].order.Order.Price;
            int GainQuantity = myOrders[i].order.Order.GainQuantity;
            int Quantity = myOrders[i].order.Order.Quantity;
            Item = Instantiate(ItemOrderbar, Table.transform);
            Debug.Log(myOrders[i].order.Order.OrderId + " " + i + " " + myOrders[i].index);
            PlayerOrderShow show = gameObject.GetComponent<PlayerOrderShow>();
            // Item.gameObject.GetComponent<UIOrderBar>().SetData(show, i, itemSO, offer_price, price_itemAVG, GainQuantity, Quantity, myOrders[i].order.Order.IsBuy, myOrders[i].order.Order.status);
        }
    }
}
