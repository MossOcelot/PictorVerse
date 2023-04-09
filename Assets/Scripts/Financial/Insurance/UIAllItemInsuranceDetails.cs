using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class UIAllItemInsuranceDetails : MonoBehaviour
{
    public TextMeshProUGUI all_price;
    public Transform content;
    public GameObject ItemPanel_template;
    GameObject ItemPanel;
    public void SetData(List<ItemInsuranceDetails> itemInsurances)
    {
        float all = 0;
        foreach(ItemInsuranceDetails item in itemInsurances)
        {
            ItemPanel = Instantiate(ItemPanel_template, content);
            UIItemPanelInsurance ui = ItemPanel.GetComponent<UIItemPanelInsurance>();
            ui.SetItem(item.item.icon, item.item.item_name, item.quantity, item.price);
            all += ((float)item.quantity * item.price);
        }
        all_price.text = all.ToString("F");
    }

    public void SetIntroduceInsurance()
    {
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class ItemInsuranceDetails
{
    public Item item;
    public int quantity;
    public float price;

    public ItemInsuranceDetails(Item newItem, int newQuentity, float newPrice) 
    { 
        item = newItem;
        quantity = newQuentity;
        price = newPrice;
    }
}