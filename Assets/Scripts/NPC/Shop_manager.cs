using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop_manager : MonoBehaviour
{
    public GameObject npc;
    public GameObject player;

    [SerializeField]
    private List<InventoryItem> player_select_items;
    [SerializeField]
    private List<InventoryItem> player_sell_items;
    public Color32[] item_tag;

    [SerializeField]
    private int price;
    [SerializeField]
    private int vat_value;
    [SerializeField]
    private int total;

    [SerializeField]
    private int sell_price;

    public GameObject buyshelf;
    public GameObject shoppingCartShelf;
    public GameObject SellShelf;
    [SerializeField]
    GameObject BagInventoryItems;
    [SerializeField]
    GameObject buyCardTemplate;
    [SerializeField]
    GameObject CartCardTemplate;
    [SerializeField]
    GameObject ItemBoxTemplate;
    GameObject buy_item_card;
    GameObject shoppingCartCard;
    GameObject ItemBag;
    GameObject SellItem;

    Button selectBtn;
    Button removeBtn;
    Button selectSellBtn;
    Button removeSellBtn;
    float VAT;
    public int n_item;
    public int[] getAccounts()
    {
        return new int[] { price, vat_value, total };
    }

    public void changeAccount(string command, int value)
    {
        if (command == "price")
        {
            this.price = value;
        } else if (command == "vat_value")
        {
            this.vat_value = value;
        } else if (command == "total")
        {
            this.total = value; 
        }
    }

    public int getSellPrice()
    {
        return this.sell_price;
    }
    public void changeSellPrice(int value)
    {
        this.sell_price = value;
    }
    public List<InventoryItem> getPlayerSelectItems()
    {
        return this.player_select_items;
    }

    public void clearPlayerSelectItem()
    {
        this.player_select_items.Clear();
    }

    public List<InventoryItem> getPlayerSellItems()
    {
        return this.player_sell_items;
    }

    public void clearPlayerSellItems()
    {
        this.player_sell_items.Clear();
    }


    public Color32 stick_tags(string rarity)
    {
        if (rarity == "common")
        {
            return item_tag[0];
        }
        else if (rarity == "rare")
        {
            return item_tag[1];
        }
        else if (rarity == "very_rare")
        {
            return item_tag[2];
        }
        else if (rarity == "super_rare")
        {
            return item_tag[3];
        }
        return item_tag[0];
    }


    // Buy Shelf
    public void OrganizeItem()
    {
        List<InventoryItem> items = npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list();
        VAT = (float)npc.gameObject.GetComponent<NPC_Shop>().VAT / 100f;
        int len = items.Count;
        for (int i = 0; i < len; i++)
        {
            InventoryItem item = items[i];
            buy_item_card = Instantiate(buyCardTemplate, buyshelf.transform);
            buy_item_card.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item.item.rarity.ToString());
            buy_item_card.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.item.icon;
            buy_item_card.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.item.name;
            buy_item_card.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item.price.ToString();
            buy_item_card.gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = item.quantity.ToString();
            selectBtn = buy_item_card.gameObject.GetComponent<Button>();
            selectBtn.AddEventListener(i, OnShopItemButtonClick);
        }
        gameObject.transform.GetChild(1).gameObject.transform.GetChild(16).gameObject.GetComponent<Text>().text = player.GetComponent<PlayerStatus>().getCash().ToString();
    }

    public void UpdateQuatityItem(int index, int value)
    {
        buyshelf.transform.GetChild(index).gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = value.ToString();
    }

    void OnShopItemButtonClick(int itemIndex)
    {
        InventoryItem item_in_stock = npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list()[itemIndex];
        

        if (item_in_stock.quantity == 0)
        {
            buyshelf.gameObject.transform.GetChild(itemIndex).gameObject.GetComponent<Button>().interactable = false;
            return;
        }
        
        if (player_select_items.Count == 0)
        {
            item_in_stock = item_in_stock.ChangeQuantity(1);
            player_select_items.Add(item_in_stock);

            price += item_in_stock.price;
            vat_value = (int)((float)price * VAT);

            total = (price + vat_value);

            shoppingCartCard = Instantiate(CartCardTemplate, shoppingCartShelf.transform);
            shoppingCartCard.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item_in_stock.item.rarity.ToString());
            shoppingCartCard.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_in_stock.item.icon;
            shoppingCartCard.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item_in_stock.item.name;
            shoppingCartCard.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item_in_stock.price.ToString();
            shoppingCartCard.gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = item_in_stock.quantity.ToString();
            removeBtn = shoppingCartCard.gameObject.GetComponent<Button>();
            removeBtn.AddEventListener(itemIndex, OnRemoveItemInCart);
        } else
        {
            int len = player_select_items.Count;
            for(int i = 0;i < len; i++)
            {
                
                if (player_select_items[i].item.item_id == item_in_stock.item.item_id)
                {
                    if (player_select_items[i].quantity >= item_in_stock.quantity)
                    {
                        buyshelf.gameObject.transform.GetChild(itemIndex).gameObject.GetComponent<Button>().interactable = false;
                        return;
                    }

                    int increaseItem = player_select_items[i].quantity + 1;
                    item_in_stock = item_in_stock.ChangeQuantity(increaseItem);
                    player_select_items[i] = item_in_stock;

                    price += item_in_stock.price;
                    vat_value = (int)((float)price * VAT);
                    total = (price + vat_value);

                    shoppingCartShelf.gameObject.transform.GetChild(i).gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = item_in_stock.quantity.ToString();
                    return;
                }
            }
            item_in_stock = item_in_stock.ChangeQuantity(1);
            player_select_items.Add(item_in_stock);

            price += item_in_stock.price;
            vat_value = (int)((float)price * VAT);
            total = (price + vat_value);

            shoppingCartCard = Instantiate(CartCardTemplate, shoppingCartShelf.transform);
            shoppingCartCard.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item_in_stock.item.rarity.ToString());
            shoppingCartCard.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_in_stock.item.icon;
            shoppingCartCard.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item_in_stock.item.name;
            shoppingCartCard.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item_in_stock.price.ToString();
            shoppingCartCard.gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = item_in_stock.quantity.ToString();
            removeBtn = shoppingCartCard.gameObject.GetComponent<Button>();
            removeBtn.AddEventListener(itemIndex, OnRemoveItemInCart);
        }
        
    }

    void OnRemoveItemInCart(int itemIndex)
    {
        InventoryItem item_in_stock = npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list()[itemIndex];

        int len = player_select_items.Count;
        for(int i = 0; i < len; i++)
        {
            if(item_in_stock.item.item_id == player_select_items[i].item.item_id)
            {
                price -= (player_select_items[i].price * player_select_items[i].quantity);
                vat_value = (int)((float)price * VAT);
                total = (price + vat_value);

                player_select_items.Remove(player_select_items[i]);
                Destroy(shoppingCartShelf.gameObject.transform.GetChild(i).gameObject);

                buyshelf.gameObject.transform.GetChild(itemIndex).gameObject.GetComponent<Button>().interactable = true;
                return;
            }
        }
    }

    // OnUnpackPlayerBag
    public void OnUnpackingPlayerBag()
    {
        List<InventoryItem> player_items = player.gameObject.GetComponent<PlayerStatus>().getItemInBag();

        int len_items = player_items.Count;
        n_item = 0;
        for (int i = 0; i < len_items; i++)
        {
            foreach(string type in npc.gameObject.GetComponent<NPC_Shop>().product_type)
            {
                if (player_items[i].item.item_type == type)
                {
                    ItemBag = Instantiate(ItemBoxTemplate, BagInventoryItems.transform.GetChild(n_item).gameObject.transform);
                    ItemBag.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = player_items[i].item.icon;
                    ItemBag.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = player_items[i].quantity.ToString();
                    selectSellBtn = ItemBag.gameObject.GetComponent<Button>();
                    selectSellBtn.AddEventListener(new int[] { i, n_item } ,OnClickChooseSell);
                    n_item++;
                }
            }
           
        }
    }

    void checkQuantityItemPlayerSell(int playerQuantity, int sellQuantity,int indexItemInBag)
    {
        if(playerQuantity == sellQuantity)
        {
            BagInventoryItems.transform.GetChild(indexItemInBag).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        }

        BagInventoryItems.gameObject.transform.GetChild(indexItemInBag).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = (sellQuantity - playerQuantity).ToString();
    }
    void OnClickChooseSell(int[] indexItem)
    {
        List<InventoryItem> player_items = player.gameObject.GetComponent<PlayerStatus>().getItemInBag();

        int len = player_sell_items.Count;
        if (len == 0)
        {
            InventoryItem firstItem = player_items[indexItem[0]].ChangeQuantity(1);
            player_sell_items.Add(firstItem);
            sell_price += firstItem.price;

            checkQuantityItemPlayerSell(player_sell_items[0].quantity, player_items[indexItem[0]].quantity, indexItem[1]);

            SellItem = Instantiate(CartCardTemplate, SellShelf.transform);

            SellItem.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(player_items[indexItem[0]].item.rarity.ToString());
            SellItem.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = player_items[indexItem[0]].item.icon;
            SellItem.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = player_items[indexItem[0]].item.name;
            SellItem.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = player_items[indexItem[0]].price.ToString();
            SellItem.gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "1";
            removeSellBtn = SellItem.gameObject.GetComponent<Button>();
            removeSellBtn.AddEventListener(indexItem, OnClickRemoveSellItem);

            return;
        }
        
        for (int i = 0; i < len; i++)
        {
            if (player_sell_items[i].item.item_id == player_items[indexItem[0]].item.item_id)
            {
                int newquantity = player_sell_items[i].quantity + 1;
                player_sell_items[i] = player_sell_items[i].ChangeQuantity(newquantity);

                sell_price += player_sell_items[i].price;

                checkQuantityItemPlayerSell(player_sell_items[i].quantity, player_items[indexItem[0]].quantity, indexItem[1]);
                // update quantity in sellshelf UI
                SellShelf.gameObject.transform.GetChild(i).gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = newquantity.ToString();
                return;
            }
        }


        InventoryItem newItem = player_items[indexItem[0]].ChangeQuantity(1);
        player_sell_items.Add(newItem);

        sell_price += newItem.price;
        checkQuantityItemPlayerSell(player_sell_items[len].quantity, player_items[indexItem[0]].quantity, indexItem[1]);
        SellItem = Instantiate(CartCardTemplate, SellShelf.transform);

        SellItem.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(player_items[indexItem[0]].item.rarity.ToString());
        SellItem.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = player_items[indexItem[0]].item.icon;
        SellItem.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = player_items[indexItem[0]].item.name;
        SellItem.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = player_items[indexItem[0]].price.ToString();
        SellItem.gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "1";
        removeSellBtn = SellItem.gameObject.GetComponent<Button>();
        removeSellBtn.AddEventListener(indexItem, OnClickRemoveSellItem);
        // btn
    }

    void OnClickRemoveSellItem(int[] indexItem)
    {
        InventoryItem player_item = player.gameObject.GetComponent<PlayerStatus>().getItemInBag()[indexItem[0]];

        int len = player_sell_items.Count;
        for (int i = 0; i < len; i++)
        {
            if (player_item.item.item_id == player_sell_items[i].item.item_id)
            {
                sell_price -= (player_sell_items[i].price * player_sell_items[i].quantity);

                player_sell_items.Remove(player_sell_items[i]);

                BagInventoryItems.transform.GetChild(indexItem[1]).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;

                BagInventoryItems.gameObject.transform.GetChild(indexItem[1]).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = player_item.quantity.ToString();

                Destroy(SellShelf.gameObject.transform.GetChild(i).gameObject);
                return;
            }
        }
    }
}
