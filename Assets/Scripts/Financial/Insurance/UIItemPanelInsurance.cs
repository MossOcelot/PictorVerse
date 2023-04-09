using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemPanelInsurance : MonoBehaviour
{
    public Image item_icon;
    public TextMeshProUGUI item_name;
    public TextMeshProUGUI item_quantity;
    public TextMeshProUGUI item_price;
    public TextMeshProUGUI all_price;

    public void SetItem(Sprite icon, string name, int quantity, float price)
    {
        item_icon.sprite = icon;
        item_name.text = name;
        item_quantity.text = quantity.ToString();
        item_price.text = price.ToString("F");
        all_price.text = ((float)quantity * price).ToString("F");
    }
}
