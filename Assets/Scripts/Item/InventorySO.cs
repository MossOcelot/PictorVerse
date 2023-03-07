using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 40;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for(int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(Item item, int quantity, List<ItemParameter> itemState = null)
        {
            if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(Item item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0;i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(Item item, int quantity)
        {
            for(int i =0; i< inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                if (inventoryItems[i].item.item_id == item.item_id)
                {
                    int amountPossibleTotake = inventoryItems[i].item.max_stack - inventoryItems[i].quantity;

                    if (quantity > amountPossibleTotake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.max_stack);
                        quantity -= amountPossibleTotake;
                    } else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.max_stack);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public bool AddItemInIndex(int index, Item item, int quantity, List<ItemParameter> itemState = null)
        {
            if (inventoryItems[index].item == null)
            {
                InventoryItem newItem = new InventoryItem
                {
                    item = item,
                    quantity = quantity,
                    itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
                };
                inventoryItems[index] = newItem;
                return true;
            }
            return false;
        }
        public void RemoveItem(int itemIndex, int amount)
        {
            if(inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                {
                    return;
                }
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if(reminder <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                } else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                }
                InformAboutChange();
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState() 
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        internal void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        internal void SortItems()
        {
            Dictionary<int, InventoryItem> sortValues = GetCurrentInventoryState();
            List<InventoryItem> newSortItems = new List<InventoryItem>();
            foreach(InventoryItem item in sortValues.Values)
            {
                newSortItems.Add(item);
            }

            int newSize = Size - newSortItems.Count;
            for (int i = 0; i < newSize; i++)
            {
                newSortItems.Add(new InventoryItem());
            }
            inventoryItems = newSortItems;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [System.Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public Item item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;
        public string owner;
        public string before_owner;
        public float price;
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                owner = this.owner,
                before_owner = this.before_owner,
                price = this.price,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public InventoryItem ChangeOwner(string newowner)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = this.quantity,
                owner = newowner,
                before_owner = this.owner,
                price = this.price,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public InventoryItem ChangePrice(float newPrice)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = this.quantity,
                owner = this.owner,
                before_owner = this.owner,
                price = newPrice,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
        {
            return new InventoryItem
            {
                item = null,
                quantity = 0,
                owner = null,
                before_owner = null,
                price = 0,
                itemState = new List<ItemParameter>()
            };
        }

    }
}


