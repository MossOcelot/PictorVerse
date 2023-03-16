using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Sell_Shelf_operation : MonoBehaviour
{
    [SerializeField]
    private Shop_manager shop;
    [SerializeField]
    private GameObject BagInventory;
    [SerializeField]
    private GameObject SellCardTemplate;
    [SerializeField]
    private GameObject SlotTemplate;
    [SerializeField]
    private Transform Selling_cart;
    [SerializeField]
    private Transform bag_location;
    [SerializeField]
    private TextMeshProUGUI total;

    [SerializeField] private InventorySO inventoryMyBag;

    private OrderOperation playerOperation;

    GameObject ItemSellCard;
    GameObject Slot_Card;
    bool IsChange;

    public float total_price;
    private void Awake()
    {
        playerOperation = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<OrderOperation>();
    }

    private void Update()
    {
        if (IsChange)
        {
            List<InventoryItem> itemSelects = shop.getPlayerSellItems();

            int count = Selling_cart.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(Selling_cart.GetChild(i).gameObject);
            }

            int n = 0;
            total_price = 0;
            foreach (InventoryItem item in itemSelects)
            {
                ItemSellCard = Instantiate(SellCardTemplate, Selling_cart);
                ItemSellCard.gameObject.GetComponent<UISellCardItem>().SetData(shop.stick_tags(item.item.item_type), item.item, item.price, item.quantity);
                ItemSellCard.gameObject.GetComponent<Button>().AddEventListener(n, OnRemoveSelectSell);

                total_price += (item.price * (float)item.quantity);
                n++;
            }
            shop.changeSellPrice(total_price);
            total.text = total_price.ToString("F");
            IsChange = false;
        }
    }

    public void SetIsChange(bool status)
    {
        IsChange = status;
    }

    void OnRemoveSelectSell(int i)
    {
        InventoryItem item = shop.getPlayerSellItems()[i];
        inventoryMyBag.AddItem(item.item, item.quantity, item.itemState);
        shop.RemovePlayerSellItem(i);

        UnPackInPlayerBag(1);
        IsChange = true;
    }

    public void UnPackInPlayerBag(int type)
    {
        Dictionary<int, InventoryItem> player_items = playerOperation.playerInventorySO.GetCurrentInventoryState();
        List<string> product_types = shop.npc.gameObject.GetComponent<NPC_Shop>().product_type;
        
        
        if(type == 0) inventoryMyBag.Initialize(); // Clear data

        int count = bag_location.childCount;
        if (count != 0)
        {
            for(int i = 0; i < count; i++)
            {
                Destroy(bag_location.GetChild(i).gameObject);
            }
        }

        int n = 0;
        foreach (InventoryItem item in player_items.Values)
        {
            bool IsType = product_types.Contains(item.item.item_type);
            Slot_Card = Instantiate(SlotTemplate, bag_location);
            Slot_Card.gameObject.GetComponent<UIInventoryItem>().SetData(n, item.item, item.item.icon, item.quantity);
            if (IsType)
            {
                Slot_Card.gameObject.GetComponent<Button>().interactable = true;
            } else
            {
                Slot_Card.gameObject.GetComponent<Button>().interactable = false;
            }
            Slot_Card.gameObject.GetComponent<Button>().AddEventListener(n, OnClickChooseSellItem);
            if (type == 0)  inventoryMyBag.AddItemInIndex(n,item.item, item.quantity, item.itemState);
            n++;
        }

        int len = player_items.Count;

        for(int i = 0; i < 40 - len; i++)
        {
            Slot_Card = Instantiate(SlotTemplate, bag_location);
        }
    }

    void OnClickChooseSellItem(int index)
    {
        InventoryItem item = inventoryMyBag.GetItemAt(index);
        inventoryMyBag.RemoveItem(index, 1);

        List<InventoryItem> Sellitems = shop.getPlayerSellItems();
        InventoryItem newItem = item.ChangeQuantity(1);

        float ItemPrice = SearchPriceData(item.item.item_id);
        newItem = newItem.ChangePrice(ItemPrice);

        int quantityItem = Sellitems.Count;
        if (quantityItem == 0)
        {
            shop.AddPlayerSellItems(newItem);
        } else
        {
            bool IsFind = false;
            for (int i = 0; i < quantityItem; i++)
            {
                if (Sellitems[i].item.item_id == newItem.item.item_id)
                {
                    int newquantity = Sellitems[i].quantity + 1;
                    Sellitems[i] = Sellitems[i].ChangeQuantity(newquantity);
                    IsFind = true;
                }
            }

            if(!IsFind) shop.AddPlayerSellItems(newItem);
        }
        if (item.quantity - 1 == 0)
        {
            bag_location.GetChild(index).gameObject.GetComponent<Button>().interactable = false;
        }
        bag_location.GetChild(index).gameObject.GetComponent<UIInventoryItem>().SetData(index, item.item, item.item.icon, item.quantity -1);
        IsChange = true;
    }

    private float SearchPriceData(int item_id)
    {
        List<InventoryItem> inventoryItems = shop.npc.gameObject.GetComponent<NPC_Shop>().getBuy_items_list();

        foreach(InventoryItem item in inventoryItems)
        {
            if(item.item.item_id == item_id)
            {
                return item.price;
            }
        }
        return 0;
    }
}
