using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using inventory.Model;
using TMPro;
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
    private float price;
    [SerializeField]
    private float vat_value;
    [SerializeField]
    private float total;

    [SerializeField]
    private float sell_price;

    [SerializeField]
    public string section_cash;

    [SerializeField]
    private List<InventoryItem> player_items;

    public GameObject buyshelf;
    public GameObject shoppingCartShelf;
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

    public TextMeshProUGUI vat_per;

    Button selectBtn;
    Button removeBtn;
    Button selectSellBtn;
    Button removeSellBtn;
    float VAT;
    public int n_item;

    private void Start()
    {
        section_cash = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();
    }
    public float[] getAccounts()
    {
        return new float[] { price, vat_value, total };
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

    public float getSellPrice()
    {
        return this.sell_price;
    }
    public void changeSellPrice(float value)
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

    public void AddPlayerSellItems(InventoryItem newItem)
    {
        this.player_sell_items.Add(newItem);    
    }

    public void UpdatePlayerSellItems(int index, InventoryItem newItem)
    {
        this.player_sell_items[index] = newItem;
    }

    public void RemovePlayerSellItem(int index)
    {
        this.player_sell_items.RemoveAt(index);
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
        // set section_cash
        section_cash = npc.gameObject.GetComponent<NPC_Status>().live_place;
        List<InventoryItem> items = npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list();
        VAT = (float)npc.gameObject.GetComponent<NPC_Shop>().VAT / 100f;
        vat_per.text = npc.gameObject.GetComponent<NPC_Shop>().VAT.ToString();
        // clear card
        int len_card = buyshelf.transform.childCount;
        for(int n = 0; n < len_card; n++)
        {
            Destroy(buyshelf.transform.GetChild(n).gameObject);
        }
        int len = items.Count;
        for (int i = 0; i < len; i++)
        {
            InventoryItem item = items[i];
            buy_item_card = Instantiate(buyCardTemplate, buyshelf.transform);
            buy_item_card.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item.item.rarity.ToString());
            buy_item_card.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.item.icon;
            buy_item_card.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item.item.name;
            buy_item_card.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item.price.ToString("F");
            buy_item_card.gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = item.quantity.ToString();
            selectBtn = buy_item_card.gameObject.GetComponent<Button>();
            selectBtn.AddEventListener(i, OnShopItemButtonClick);
        }
        gameObject.transform.GetChild(1).gameObject.transform.GetChild(16).gameObject.GetComponent<TextMeshProUGUI>().text = player.GetComponent<PlayerStatus>().player_accounts.getPocket()[section_cash].ToString("F");

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
            vat_value = (float)price * VAT;

            total = (price + vat_value);

            shoppingCartCard = Instantiate(CartCardTemplate, shoppingCartShelf.transform);
           
            shoppingCartCard.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item_in_stock.item.rarity.ToString());
            shoppingCartCard.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_in_stock.item.icon;
            shoppingCartCard.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item_in_stock.item.name;
            shoppingCartCard.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item_in_stock.price.ToString("F");
            
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
                    vat_value = (float)price * VAT;
                    total = (price + vat_value);

                    shoppingCartShelf.gameObject.transform.GetChild(i).gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = item_in_stock.quantity.ToString();
                    return;
                }
            }
            item_in_stock = item_in_stock.ChangeQuantity(1);
            player_select_items.Add(item_in_stock);

            price += item_in_stock.price;
            vat_value = (float)price * VAT;
            total = (price + vat_value);

            shoppingCartCard = Instantiate(CartCardTemplate, shoppingCartShelf.transform);
            shoppingCartCard.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = stick_tags(item_in_stock.item.rarity.ToString());
            shoppingCartCard.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_in_stock.item.icon;
            shoppingCartCard.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = item_in_stock.item.name;
            shoppingCartCard.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = item_in_stock.price.ToString("F");
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
                vat_value = (float)price * VAT;
                total = (price + vat_value);

                player_select_items.Remove(player_select_items[i]);
                Destroy(shoppingCartShelf.gameObject.transform.GetChild(i).gameObject);

                buyshelf.gameObject.transform.GetChild(itemIndex).gameObject.GetComponent<Button>().interactable = true;
                return;
            }
        }
    }
}
