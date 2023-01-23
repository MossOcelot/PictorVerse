using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraInventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;

    public Toggle RemoveMenu;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = false;

        if (RemoveMenu.isOn)
        {
            removeButton.interactable = true;
            removeButton.gameObject.SetActive(true);
        }
        else
        {
            removeButton.interactable = false;
            removeButton.gameObject.SetActive(false);
        }
    }
    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        removeButton.gameObject.SetActive(false);
    }

    public void RemoveItemFromExtraInventory()
    {
        ExtraInventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
