using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_Sell_Shelf : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private Text total_text;
    [SerializeField]
    private GameObject sellShelf;
    [SerializeField]
    private GameObject InventoryBag;
    [SerializeField]
    public string section_cash;
    Button confirm_sell;
    float total_value;
    // Start is called before the first frame update
    void Start()
    {
        confirm_sell = gameObject.GetComponent<Button>();
        confirm_sell.AddEventListener(1, OnClickConfirmSell);

        // get section cash
        section_cash = background.gameObject.GetComponent<Shop_manager>().section_cash;
    }

    private void FixedUpdate()
    {
        total_value = (float)background.GetComponent<Shop_manager>().getSellPrice() * 0.7f;
        total_text.text = total_value.ToString("F");
    }

    void OnClickConfirmSell(int indexItem)
    {
        Shop_manager SM = background.gameObject.GetComponent<Shop_manager>();
        NPC_Shop NS = SM.npc.gameObject.GetComponent<NPC_Shop>();
        List<InventoryItem> buy_item_list = SM.npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list();
        List<InventoryItem> playerBag = SM.player.gameObject.GetComponent<PlayerStatus>().getItemInBag();

        SM.changeSellPrice(0);

        foreach (InventoryItem item in SM.getPlayerSellItems())
        {
            InventoryItem new_item = item.ChangeOwner("NPC");

            // update stock player
            int len_Item_InBag = playerBag.Count;
            for (int i = 0; i < len_Item_InBag; i++)
            {
                if (playerBag[i].item.item_id == new_item.item.item_id)
                {
                    int remain = playerBag[i].quantity - new_item.quantity;
                    if (remain == 0)
                    {
                        background.gameObject.GetComponent<Shop_manager>().player.gameObject.GetComponent<PlayerStatus>().deleteItemInBag(playerBag[i]);
                    } 
                    else
                    {
                        playerBag[i] = playerBag[i].ChangeQuantity(remain);
                    }


                    break;
                }
            }



            // Update item NPC
            int len_buy_item_list = buy_item_list.Count;
            for (int i = 0; i < len_buy_item_list; i++)
            {
                if (buy_item_list[i].item.item_id == new_item.item.item_id)
                {
                    int remain = buy_item_list[i].quantity + new_item.quantity;
                    buy_item_list[i] = buy_item_list[i].ChangeQuantity(remain);

                    
                    break;
                }
            }
        }

        // get date time
        int[] dateTime = GetDateTime();

        // Update cash Player
        float newCash = SM.player.gameObject.GetComponent<PlayerStatus>().player_accounts.getPocket()[section_cash] + total_value;
        SM.player.gameObject.GetComponent<PlayerStatus>().player_accounts.setPocket(section_cash,newCash);
        // Update Accounts Player
        AccountsDetail account_player = new AccountsDetail() { date= dateTime, accounts_name="sell items", account_type="sell", income= total_value, expense=0};
        background.gameObject.GetComponent<Shop_manager>().player.gameObject.GetComponent<PlayerStatus>().addAccountsDetails(account_player);

        // update cash NPC
        float newbalance = NS.GetFinancial_balance() - total_value;
        NS.setFinancial_detail("balance", newbalance);
        // Update Accounts Player
        AccountsDetail account_NPC = new AccountsDetail() { date = dateTime, accounts_name = "buy items", account_type = "buy", income = 0, expense = total_value };
        background.gameObject.GetComponent<Shop_manager>().npc.gameObject.GetComponent<NPC_Shop>().addAccountsDetails(account_NPC);

        // clear sell shelf
        SM.clearPlayerSellItems();
        int len = sellShelf.transform.childCount;
        for (int i = 0; i < len; i++)
        {
            Destroy(sellShelf.transform.GetChild(i).gameObject);
        }


        // update InventoryBag
        int len_item = background.gameObject.GetComponent<Shop_manager>().n_item;
        for (int i = 0;i < len_item; i++)
        {
            Destroy(InventoryBag.gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject);
        }
        background.gameObject.GetComponent<Shop_manager>().OnUnpackingPlayerBag();
    }

    int[] GetDateTime()
    {
        Timesystem date = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        return date.getDateTime();
    }
}
