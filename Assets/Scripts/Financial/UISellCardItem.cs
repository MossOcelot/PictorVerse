using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using inventory.Model;

public class UISellCardItem : MonoBehaviour
{
    [SerializeField]
    private Image item_tag;
    [SerializeField]
    private Image item_icon;
    [SerializeField]
    private Text item_name;
    [SerializeField] 
    private Text price_value;
    [SerializeField]
    private Text quantity;

    public void SetData(Color32 color_tag, Item newitem, float price, int quantity)
    {
        this.item_tag.color = color_tag;
        this.item_icon.sprite = newitem.icon;
        this.item_name.text = newitem.item_name;
        this.price_value.text = price.ToString("F");
        this.quantity.text = quantity.ToString();
    }
}
