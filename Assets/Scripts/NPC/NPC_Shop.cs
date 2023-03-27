using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using inventory.Model;
using TMPro;

public class NPC_Shop : MonoBehaviour
{
    public NPC_Status NPC_status;
    [SerializeField]
    private InventorySO playerBag;
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
    [SerializeField]
    private List<AccountsDetail> accountsDetails;
    [SerializeField]
    public string section_cash;

    public int VAT;
    Collider2D player;

    GameObject shop;
    public GameObject goverment;

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

    public void setFinancial_detail(string command, float value)
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

    public float GetFinancial_balance()
    {
        return this.financial_shop_detail.balance;
    }

    public float GetFinancial_debt()
    {
        return this.financial_shop_detail.debt;
    }

    public List<AccountsDetail> getAccountsDetails()
    {
        return this.accountsDetails;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        this.accountsDetails.Insert(0, account);
    }

    private void Start()
    {
        goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject;
        VAT = goverment.GetComponent<GovermentPolicy>().getVat();

        // get section cash
        section_cash = NPC_status.live_place;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            player = target;
        }
    }

    public void OpenShelf()
    {
        shop = Instantiate(myshop, shop_manager);
        shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().player = player.gameObject;
        shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().npc = gameObject;
        shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>().OrganizeItem();
        shop.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = NPC_status.npc_img;
        cofirmBtn = shop.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(18).gameObject.GetComponent<Button>();
        shop.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(16).gameObject.GetComponent<TextMeshProUGUI>().text = player.GetComponent<PlayerStatus>().player_accounts.getPocket()[section_cash].ToString("F");
        cofirmBtn.AddEventListener(player.gameObject, OnShopConfirmBuy);
    }

    void OnShopConfirmBuy(GameObject player)
    {
        // get Time date
        Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        int[] dateTime = date.getDateTime();

        PlayerStatus status = player.GetComponent<PlayerStatus>();
        Shop_manager back_shop = shop.transform.GetChild(0).gameObject.GetComponent<Shop_manager>();
        float total = back_shop.getAccounts()[2];
        float balance = status.player_accounts.getPocket()[section_cash] - total;
        status.player_accounts.setPocket(section_cash,balance);
        shop.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(16).gameObject.GetComponent<TextMeshProUGUI>().text = status.player_accounts.getPocket()[section_cash].ToString("F");
        float static_buy = status.getMyStatic().static_spendBuy + total;
        float vat_value = back_shop.getAccounts()[1];
        float static_vat = status.getMyStatic().static_spendVAT + vat_value;
        status.setMyStatic(1, static_buy);
        if (vat_value != 0)
        {
            status.setMyStatic(2, static_vat);

            // pay to goverment
            float total_goverment_balance = goverment.GetComponent<GovermentStatus>().getGovermentFinancial_balance() + vat_value;
            goverment.GetComponent<GovermentStatus>().setFinancial_detail("balance", total_goverment_balance);
            AccountsDetail account_goverment = new AccountsDetail() { date = dateTime, accounts_name = "vat", account_type = "vat", income = vat_value, expense = 0 };
            goverment.GetComponent<GovermentStatus>().addAccountsDetail(account_goverment);
        
        }

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
            playerBag.AddItem(new_item);
        }
        back_shop.clearPlayerSelectItem();
        int len = back_shop.shoppingCartShelf.transform.childCount;
        for ( int i = 0;i < len;i++)
        {
            Destroy(back_shop.shoppingCartShelf.transform.GetChild(i).gameObject);
        }
        float price = total - vat_value;
        financial_shop_detail.balance += price;
        // Update Accounts Player
        AccountsDetail account_Player = new AccountsDetail() { date = dateTime, accounts_name = "buy items", account_type = "buy", income = 0, expense = total };
        player.gameObject.GetComponent<PlayerStatus>().addAccountsDetails(account_Player);
        // Update Accounts NPC
        AccountsDetail account_NPC = new AccountsDetail() { date = dateTime, accounts_name = "sell items", account_type = "sell", income = price, expense =  0};
        addAccountsDetails(account_NPC);
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
