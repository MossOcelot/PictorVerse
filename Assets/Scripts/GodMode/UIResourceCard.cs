using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIResourceCard : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemQuantity;
    public TextMeshProUGUI ItemPrice;

    public void SetData(Sprite sprite, string itemName, int itemQuantity, float itemPrice)
    {
        icon.sprite = sprite;
        ItemName.text = itemName;
        ItemQuantity.text = $"ปริมาณ: {itemQuantity}";
        ItemPrice.text = $"ราคา: {itemPrice}";
    }
}
