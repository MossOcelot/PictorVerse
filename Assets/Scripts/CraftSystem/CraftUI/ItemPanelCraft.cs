using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemPanelCraft : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI item_name;
    public TextMeshProUGUI item_quantity;
    public void SetData (Sprite newIcon, string name, int quantity)
    {
        icon.sprite = newIcon;
        item_name.text = name;
        item_quantity.text = quantity.ToString();
    }
}
