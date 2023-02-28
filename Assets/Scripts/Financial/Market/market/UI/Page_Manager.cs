using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Manager : MonoBehaviour
{
    public ItemStock ItemInStock;
    public StockSystem stockSystem;
    public Paper_Manager paper;
    public Paper_manager2 paper2;
    public Button BuyButton;
    public Button SellButton;
    public void setDataInPage()
    {
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = ItemInStock.item.item_name;
    }

}
