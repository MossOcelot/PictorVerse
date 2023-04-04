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
        public Item item;
        public int item_amount;
        public int indexSlot;
    }
    public void OnDrop(PointerEventData eventData) 
    {
        
        Item item = UIInventoryItem.Instance.GetItem();
        int index = UIInventoryItem.Instance.GetIndex();
        int amount = UIInventoryItem.Instance.GetItemAmount();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = item, item_amount = amount, indexSlot = index });
    }
}

