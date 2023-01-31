using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Shop : MonoBehaviour
{
    [SerializeField]
    private NPC_Status NPC_status;
    [SerializeField]
    private Financial_Details financial_shop_detail;
    [SerializeField]
    private List<InventoryItem> buy_items_list;
    [SerializeField]
    private List<InventoryItem> resource;
    public List<string> product_type;
    [SerializeField] 
    private Transform shop_manager;
    [SerializeField]
    private GameObject myshop;
    
    public int VAT;

    GameObject shop;

    Button cofirmBtn;
    public void setBuy_items_list(int index,InventoryItem inventoryitem)
    {
        this.buy_items_list[index] = inventoryitem;
    }

    public void addBuy_items_list(InventoryItem inventoryitem)
    {
        this.buy_items_list.Add(inventoryitem);
    }

    public void deleteBuy_items_list(InventoryItem inventoryitem)
    {
        this.buy_items_list.Remove(inventoryitem);
    }
    public List<InventoryItem> getBuy_items_list()
    {
        return this.buy_items_list;
    }

    public void setResource(int index, InventoryItem resourceitem)
    {
        this.resource[index] = resourceitem;
    }

    public void addResource(InventoryItem resourceitem)
    {
        this.resource.Add(resourceitem);
    }

    public void deleteResource(InventoryItem resourceitem)
    {
        this.resource.Remove(resourceitem);
    }
    public List<InventoryItem> getResource()
    {
        return this.resource;
    }

    public void setFinancial_detail(string command, int value)
    {
        if (command == "balance")
        {
            this.financial_shop_detail.balance = value;
        }
        else if (command == "debt")
        {
            this.financial_shop_detail.debt = value;
        }
    }

    public int GetFinancial_balance()
    {
        return this.financial_shop_detail.balance;
    }

    public int GetFinancial_debt()
    {
        return this.financial_shop_detail.debt;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            shop = Instantiate(myshop, shop_manager);
            shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().player = target.gameObject;
            shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().npc = gameObject;
            shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().OrganizeItem();
            shop.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = NPC_status.npc_img;
            cofirmBtn = shop.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(18).gameObject.GetComponent<Button>();
            cofirmBtn.AddEventListener(target.gameObject, OnShopConfirmBuy);
        }
    }

    void OnShopConfirmBuy(GameObject player)
    {
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        Shop_manager back_shop = shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>();

        int total = back_shop.getAccounts()[2];
        int balance = status.getCash() - total;
        status.changeCash(balance);
        shop.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(16).gameObject.GetComponent<Text>().text = status.getCash().ToString();

        int static_buy = status.getMyStatic()["static_SpendBuy"] + total;
        int vat_value = back_shop.getAccounts()[1];
        int static_vat = status.getMyStatic()["static_SpendVat"] + vat_value;
        status.setMyStatic(1, static_buy);
        status.setMyStatic(2, static_vat);

        List<InventoryItem> playerItems = back_shop.getPlayerSelectItems();
        foreach(InventoryItem item in playerItems )
        {
            InventoryItem new_item = item.ChangeOwner(status.name);

            // update stock
            int len_buy_item_list = buy_items_list.Count;
            for(int i = 0; i < len_buy_item_list; i++)
            {
                if (buy_items_list[i].item.item_id == new_item.item.item_id)
                {
                    int remain = buy_items_list[i].quantity - new_item.quantity;
                    buy_items_list[i] = buy_items_list[i].ChangeQuantity(remain);

                    back_shop.UpdateQuatityItem(i, remain);
                    break;
                }
            }
            // item in bag player
            if (status.getItemInBag().Count == 0)
            {
                status.addItemInBag(new_item);
                continue;
            }

            int len_ItemBag = status.getItemInBag().Count;
            int n = 0;
            for (int i = 0; i < len_ItemBag; i++)
            {
                if (status.getItemInBag()[i].item.item_id == new_item.item.item_id)
                {
                    // not have over stack function
                    int newQuantity = status.getItemInBag()[i].quantity + new_item.quantity;
                    InventoryItem updateItem = status.getItemInBag()[i].ChangeQuantity(newQuantity);
                    status.setItemInBag(i, updateItem);
                    n += 1;
                }
            }
            if (n == 0) {
                status.addItemInBag(new_item);
            }
        }
        back_shop.clearPlayerSelectItem();

        int len = back_shop.shoppingCartShelf.transform.childCount;
        for ( int i = 0;i < len;i++)
        {
            Destroy(back_shop.shoppingCartShelf.transform.GetChild(i).gameObject);
        }

        financial_shop_detail.balance += (total - vat_value);
        // clearAccountShop
        back_shop.changeAccount("price",0);
        back_shop.changeAccount("vat_value", 0);
        back_shop.changeAccount("total", 0);
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("ShopManager").gameObject.transform.GetChild(0).gameObject);
        }
    }
}
