using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderBar : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text item_name;
    [SerializeField]
    private Text sellprice;
    [SerializeField]
    private Text price;
    [SerializeField]
    private Text amount;
    [SerializeField]
    private GameObject[] OrderType;
    [SerializeField]
    private GameObject[] OrderStatus;

    private StockSystem stockSystem;
    private OrderOperation PlayerOrder;
    private data_storage storage;
    public Item NewItem;
    public float AVG_Price;
    public float Offer_price;
    public int GainQuantity;
    public int Quantity;
    public bool Type_order;
    public int Item_status;

    Button ItemOrderBtn;

    private void Start()
    {
        PlayerOrder = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<OrderOperation>();
        stockSystem = GameObject.FindGameObjectWithTag("market_manager").gameObject.GetComponent<StockSystem>();
        storage = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(1).gameObject.GetComponent<data_storage>();
    }
    public void SetData(int index_myOrderList, Item newItem, float offer_price,float AVG_price, int Gainquantity, int quantity, bool type_order, int item_status)
    {
        // Update Status
        NewItem = newItem;
        Offer_price = offer_price;
        AVG_Price = AVG_price;
        GainQuantity = Gainquantity;
        Quantity = quantity;
        Type_order = type_order;
        Item_status = item_status;

        // Update UI
        this.image.sprite = newItem.icon;
        this.item_name.text = newItem.name;

        this.sellprice.text = offer_price.ToString("F");
        this.price.text = AVG_price.ToString("F");
        this.amount.text = Gainquantity.ToString() + "/" + (quantity + Gainquantity).ToString();

        if (type_order)
        {
            this.OrderType[0].SetActive(true);
            this.OrderType[1].SetActive(false);
        } else
        {
            this.OrderType[0].SetActive(false);
            this.OrderType[1].SetActive(true);
        }

        if(item_status == 1)
        {
            this.OrderStatus[0].SetActive(false);
            this.OrderStatus[1].SetActive(false);
            this.OrderStatus[2].SetActive(true);
        } else if (item_status == 2)
        {
            this.OrderStatus[0].SetActive(false);
            this.OrderStatus[1].SetActive(true);
            this.OrderStatus[2].SetActive(false);
        } else if (item_status == 3)
        {
            this.OrderStatus[0].SetActive(true);
            this.OrderStatus[1].SetActive(false);
            this.OrderStatus[2].SetActive(false);
        }

        ItemOrderBtn = gameObject.GetComponent<Button>();
        ItemOrderBtn.AddEventListener(index_myOrderList, OnClickConfirm);

    }

    private void OnClickConfirm(int i)
    {
        if (Type_order)
        {
            FunctionIsBuy(i);
        } else
        {
            FunctionIsSell(i);
        }

        GameObject.FindGameObjectWithTag("market_manager").gameObject.GetComponent<MyOrderShow>().SetIsMyOrder(false);
    }

    private void FunctionIsBuy(int i)
    {
        int quantity = PlayerOrder.playerInventorySO.AddItem(NewItem, GainQuantity);
        if(quantity > 0) 
        {
            GainQuantity -= quantity;
            Quantity -= quantity;

            this.amount.text = GainQuantity.ToString() + "/" + (Quantity + GainQuantity).ToString();

            float newPrice = stockSystem.getBalance() + (Offer_price - AVG_Price);
            Debug.Log("Offer_price1: " + Offer_price + " AVG_Price: " + AVG_Price);
            stockSystem.setBalance(newPrice);

            storage.updateStatusBrokerOrder(i, 3, false);
        } 
        else
        {
            float newPrice = stockSystem.getBalance() + (Offer_price - AVG_Price);
            stockSystem.setBalance(newPrice);
            Debug.Log("Offer_price2: " + Offer_price + " AVG_Price: " + AVG_Price);
            storage.updateStatusBrokerOrder(i, 3, false);
        }

        
    }

    private void FunctionIsSell(int i)
    {

        if (Quantity == 0)
        {
            float newPrice = stockSystem.getBalance() + AVG_Price;
            stockSystem.setBalance(newPrice);
        } else {
            float newPrice = stockSystem.getBalance() + AVG_Price;
            stockSystem.setBalance(newPrice);
            PlayerOrder.playerInventorySO.AddItem(NewItem, Quantity);
        }

        storage.updateStatusBrokerOrder(i, 3, false);
    }
}
