using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myBag_in_shop : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject slotItemTemplate;
    [SerializeField]
    private GameObject cartCardTemplate;
    [SerializeField]
    private GameObject selling_cart;
    GameObject player;
    GameObject slot_item;
    GameObject CartCard;

    Button selectBtn;
    List<InventoryItem> items;
    List<InventoryItem> itemsPlayerSell;
    // Start is called before the first frame update
    public void OnUnpackInBag()
    {
        player = background.gameObject.GetComponent<Shop_manager>().player;
        items = player.gameObject.GetComponent<PlayerStatus>().getBag();
        int len = items.Count;
        for (int i = 0; i < len; i++)
        {
            GameObject slot = gameObject.transform.GetChild(i).gameObject;
            slot_item = Instantiate(slotItemTemplate, slot.transform);
            slot_item.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = items[i].item.icon;
            slot_item.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = items[i].quantity.ToString();
            selectBtn = slotItemTemplate.gameObject.GetComponent<Button>();
            selectBtn.interactable = true;
            selectBtn.AddEventListener(i, onSelectSellItem);
        }
    }


    void onSelectSellItem(int i)
    {
        int len_selling_Card = selling_cart.gameObject.transform.childCount;
        if (len_selling_Card == 0)
        {
            int newprice = items[i].price;
            background.gameObject.GetComponent<Shop_manager>().changeSellPrice(newprice);

            items[i] = items[i].ChangeQuantity(1);
            itemsPlayerSell.Add(items[i]);

            CartCard = Instantiate(cartCardTemplate, selling_cart.transform);
            CartCard.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = background.gameObject.GetComponent<Shop_manager>().stick_tags(items[i].item.rarity.ToString());
            CartCard.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = items[i].item.icon;
            CartCard.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = items[i].item.name;
            CartCard.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = items[i].price.ToString();
            CartCard.gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "1";
            return;
        }

        for (int card = 0; card < len_selling_Card; card++)
        {
            if (itemsPlayerSell[card].item.item_id == items[i].item.item_id)
            {
                selling_cart.gameObject.transform.GetChild(card).gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "1";
                return;
            }
        }

        OnUnpackInBag();
    }
}
