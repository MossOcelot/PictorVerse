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

    SceneStatus sceneStatus;
    private OrderOperation PlayerOrder;
    private data_storage storage;
    public Item NewItem;
    public float AVG_Price;
    public int GainQuantity;
    public int Quantity;
    public bool Type_order;
    public int Item_status;

    Button ItemOrderBtn;

    private void Start()
    {
        sceneStatus = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
        PlayerOrder = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<OrderOperation>();
        storage = GameObject.FindGameObjectWithTag("Broker").gameObject.transform.GetChild(1).gameObject.GetComponent<data_storage>();
    }
    public void SetData(int index_myOrderList, Item newItem, float offer_price,float AVG_price, int Gainquantity, int quantity, bool type_order, int item_status)
    {
        // Update Status
        NewItem = newItem;
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

        Debug.Log("typeee " + type_order);
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
    }

    private void FunctionIsBuy(int i)
    {
        string sectionName = sceneStatus.sceneInsection.ToString();
        int quantity = PlayerOrder.playerInventorySO.AddItem(NewItem, GainQuantity);
        if(quantity > 0) 
        {
            GainQuantity -= quantity;
            Quantity -= quantity;

            this.amount.text = GainQuantity.ToString() + "/" + (Quantity + GainQuantity).ToString();
        } 
        else
        {
            // ของครบ เข้ากระเป๋าหมด
            DeleteOrderBar(i);
        }

        float newPrice = PlayerOrder.player.player_accounts.getPocket()[sectionName] - AVG_Price;
        PlayerOrder.player.player_accounts.setPocket(sectionName, newPrice);

        storage.updateStatusBrokerOrder(i, 3, false);
    }

    private void FunctionIsSell(int i)
    {
        string sectionName = sceneStatus.sceneInsection.ToString();
        float newPrice = PlayerOrder.player.player_accounts.getPocket()[sectionName] + AVG_Price;
        PlayerOrder.player.player_accounts.setPocket(sectionName, newPrice);

        if (Quantity > 0)
        {
            PlayerOrder.playerInventorySO.AddItem(NewItem, Quantity);
        }

        storage.updateStatusBrokerOrder(i, 3, false);
        DeleteOrderBar(i);
    }

    public void DeleteOrderBar(int i)
    {
        storage.removeBrokerOrder(i);
        Destroy(gameObject);
    }
}
