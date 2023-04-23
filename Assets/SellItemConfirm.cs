using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SellItemConfirm : MonoBehaviour
{
    [SerializeField]
    private Shop_manager shop_manager;
    [SerializeField]
    private Sell_Shelf_operation sell_shelf_operation;
    private InventorySO inventory_player;

    private SceneStatus.section section;
    private void Start()
    {
        inventory_player = shop_manager.player.gameObject.GetComponent<OrderOperation>().playerInventorySO;
        section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }

    public void SellConfirmBtn()
    {
        List<InventoryItem> items = shop_manager.getPlayerSellItems();
        Dictionary<int, InventoryItem> Player_item = inventory_player.GetCurrentInventoryState();
        foreach (InventoryItem item in items)
        {
            int quantity = item.quantity;
            
            foreach (int n_item in Player_item.Keys)
            {
                
                if (Player_item[n_item].item.item_id == item.item.item_id)
                {
                    if (quantity > Player_item[n_item].quantity)
                    {
                        quantity = quantity - Player_item[n_item].item.max_stack;
                        inventory_player.RemoveItem(n_item, Player_item[n_item].quantity);
                    } else
                    {
                        inventory_player.RemoveItem(n_item, item.quantity);
                        break;
                    }
                }
            }
            
        }
        Timesystem date = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        int[] dateTime = date.getDateTime();

        AccountsDetail account_Player = new AccountsDetail() { date = dateTime, accounts_name = "sell items", account_type = "MI", income = shop_manager.getSellPrice(), expense = 0 , currencyIncome_Type = section, currencyExpense_Type = section };
        shop_manager.player.gameObject.GetComponent<PlayerStatus>().addAccountsDetails(account_Player);
        // Update Accounts NPC
        AccountsDetail account_NPC = new AccountsDetail() { date = dateTime, accounts_name = "buy items", account_type = "buy", income = 0, expense = shop_manager.getSellPrice() , currencyIncome_Type = section, currencyExpense_Type = section };
        shop_manager.npc.gameObject.GetComponent<NPC_Shop>().addAccountsDetails(account_NPC);
        float newcash_player = shop_manager.player.gameObject.GetComponent<PlayerStatus>().player_accounts.getPocket()[shop_manager.section_cash] + sell_shelf_operation.total_price;
        shop_manager.player.gameObject.GetComponent<PlayerStatus>().player_accounts.setPocket(shop_manager.section_cash, newcash_player);
        
        float newCash_npc = shop_manager.npc.gameObject.GetComponent<NPC_Shop>().GetFinancial_balance() - sell_shelf_operation.total_price;
        shop_manager.npc.gameObject.GetComponent<NPC_Shop>().setFinancial_detail("balance" , newCash_npc);


        shop_manager.clearPlayerSellItems();
    }
}
