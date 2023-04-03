using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOrderShow : MonoBehaviour
{
    [System.Serializable]
    public class MyOrderShowIndex
    {
        public int index;
        public BrokerOrder brokerOrder;

        public MyOrderShowIndex(int index, BrokerOrder brokerOrder)
        {
            this.index = index;
            this.brokerOrder = brokerOrder;
        }
    }

    [SerializeField]
    private GameObject Table;
    public StockSystem stock;
    public GameObject ItemOrderbar;

    public bool IsMyOrder = false;
    public List<MyOrderShowIndex> myOrderShowIndices;
    GameObject item;
    PlayerStatus player_status;
    data_storage storage;

    public void SetIsMyOrder(bool status)
    {
        this.IsMyOrder = status;
    }
    private void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        storage = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(1).gameObject.GetComponent<data_storage>();
    }
    private void FixedUpdate()
    {
        if (!IsMyOrder)
        {
            showMyOrder();
        }
    }

    private void getOrderStorage()
    {
        List<BrokerOrder> newStorage = storage.getListBrokerOrder();

        int len = newStorage.Count;

        for(int i = 0; i < len; i++)
        {
            if(newStorage[i].Order.Customer == null)
            {
                break;
            }
            PlayerStatus player = newStorage[i].Order.Customer.player;
            if(player != null )
            {
                if (player.player_id == player_status.player_id)
                {
                    MyOrderShowIndex order_index = new MyOrderShowIndex(i, newStorage[i]);
                    myOrderShowIndices.Add(order_index);
                }
            }
        }
    }

    public void showMyOrder()
    {
        myOrderShowIndices = new List<MyOrderShowIndex>();
        getOrderStorage();

        int len_myOrder = Table.transform.childCount;
        if (len_myOrder != 0)
        {
            for (int i = 0; i < len_myOrder; i++)
            {
                Destroy(Table.transform.GetChild(i).gameObject);
            }
        }
        int len = myOrderShowIndices.Count;
        for (int i = 0; i < len; i++)
        {
            Item itemSO = new Item();
            foreach (ItemInStock itemInStock in stock.stock)
            {
                foreach (ItemStock item in itemInStock.itemStock)
                {
                    bool isfind = false;
                    if (int.Parse(myOrderShowIndices[i].brokerOrder.Order.OrderId.Substring(2, 5)) == item.item.item_id)
                    {
                        itemSO = item.item;
                        isfind = true;
                        break;
                    }
                    if (isfind)
                    {
                        break;
                    }
                }
            }
            float price_itemAVG = myOrderShowIndices[i].brokerOrder.Order.GainQuantity * myOrderShowIndices[i].brokerOrder.Order.Price;
            float offer_price = (myOrderShowIndices[i].brokerOrder.Order.Quantity + myOrderShowIndices[i].brokerOrder.Order.GainQuantity) * myOrderShowIndices[i].brokerOrder.Order.Price;
            int GainQuantity = myOrderShowIndices[i].brokerOrder.Order.GainQuantity;
            int Quantity = myOrderShowIndices[i].brokerOrder.Order.Quantity;
            item = Instantiate(ItemOrderbar, Table.transform);
            PlayerOrderShow show = gameObject.GetComponent<PlayerOrderShow>();
            item.gameObject.GetComponent<UIOrderBar>().SetData(myOrderShowIndices[i].index, itemSO, offer_price, price_itemAVG, 
                GainQuantity, Quantity, myOrderShowIndices[i].brokerOrder.Order.IsBuy, myOrderShowIndices[i].brokerOrder.Order.status);
        }
        IsMyOrder = true;
    }
}
