using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UI_CharacterEquipmentSlot : MonoBehaviour, IDropHandler
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public string name;
        public Item item;
        public int item_amount;
        public int indexSlot;
    }
    public void OnDrop(PointerEventData eventData) 
    {
        string itemUI_name = UIInventoryItem.Instance.name;
        Item item = UIInventoryItem.Instance.GetItem();
        int index = UIInventoryItem.Instance.GetIndex();
        int amount = UIInventoryItem.Instance.GetItemAmount();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { name = itemUI_name, item = item, item_amount = amount, indexSlot = index });
    }
}

