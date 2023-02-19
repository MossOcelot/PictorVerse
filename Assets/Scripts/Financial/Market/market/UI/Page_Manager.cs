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
        BuyButton.AddEventListener(1, confirmBuy);
        SellButton.AddEventListener(1, confirmSell);
    }

    void confirmBuy(int i)
    {
        string market_name = stockSystem.market_name;
        stockSystem.player.setStatus("player", market_name, ItemInStock.item.item_id, paper.price, (int)paper.quantity, true);
        paper.price = 0;
        paper.quantity = 0;
        paper.priceInput.gameObject.GetComponent<InputField>().text = "0";
        paper.quantityInput.gameObject.GetComponent<InputField>().text = "0";

    }

    void confirmSell(int i)
    {
        string market_name = stockSystem.market_name;
        stockSystem.player.setStatus("player", market_name, ItemInStock.item.item_id, paper2.price, (int)paper2.quantity, false);
        paper2.price = 0;
        paper2.quantity = 0;
        paper2.priceInput.gameObject.GetComponent<InputField>().text = "0";
        paper2.quantityInput.gameObject.GetComponent<InputField>().text = "0";
    }


}
