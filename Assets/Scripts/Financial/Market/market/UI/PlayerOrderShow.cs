using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOrderShow : MonoBehaviour
{
    public StockSystem stock;
    [SerializeField]
    private OrderOperation player;
    public List<BrokerOrder> playerOrder;
    public GameObject Table;
    public GameObject ItemOrderbar;

    GameObject Item;
    public void ShowPlayerOrder()
    {
        Debug.Log("A");
        Debug.Log(Table.transform.childCount);
        int len_myOrder = Table.transform.childCount;
        if (len_myOrder != 0)
        {
            
            for(int i = 0; i < len_myOrder; i++)
            {
                Destroy(Table.transform.GetChild(i).gameObject);
            }
        }

        player = stock.player;
        playerOrder = player.myOrders;
        int len = playerOrder.Count;
        for(int i = 0; i < len; i++)
        {
            Item itemSO = new Item();
            foreach(ItemInStock itemInStock in stock.stock)
            {
                foreach(ItemStock item in itemInStock.itemStock)
                {
                    bool isfind = false;
                    if(int.Parse(playerOrder[i].Order.OrderId.Substring(2, 5)) == item.item.item_id)
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
            Item = Instantiate(ItemOrderbar, Table.transform);
            Item.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSO.icon;
            Item.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemSO.item_name;
            if (playerOrder[i].Order.IsBuy)
            {
                Item.gameObject.transform.GetChild(7).gameObject.SetActive(true);
                Item.gameObject.transform.GetChild(8).gameObject.SetActive(false);
            } else
            {
                Item.gameObject.transform.GetChild(7).gameObject.SetActive(false);
                Item.gameObject.transform.GetChild(8).gameObject.SetActive(true);
            }
            Item.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = ((playerOrder[i].Order.Quantity + playerOrder[i].Order.GainQuantity) * playerOrder[i].Order.Price).ToString();
            Item.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = playerOrder[i].Order.GainQuantity.ToString() + "/" + (playerOrder[i].Order.Quantity + playerOrder[i].Order.GainQuantity).ToString();
            if (playerOrder[i].Order.status == 1)
            {
                Item.gameObject.transform.GetChild(9).gameObject.SetActive(false);
                Item.gameObject.transform.GetChild(10).gameObject.SetActive(false);
                Item.gameObject.transform.GetChild(11).gameObject.SetActive(true);
            } else if(playerOrder[i].Order.status == 2)
            {
                Item.gameObject.transform.GetChild(9).gameObject.SetActive(false);
                Item.gameObject.transform.GetChild(10).gameObject.SetActive(true);
                Item.gameObject.transform.GetChild(11).gameObject.SetActive(false);
            } else
            {
                Item.gameObject.transform.GetChild(9).gameObject.SetActive(true);
                Item.gameObject.transform.GetChild(10).gameObject.SetActive(false);
                Item.gameObject.transform.GetChild(11).gameObject.SetActive(false);
            }
        }
    }
}
