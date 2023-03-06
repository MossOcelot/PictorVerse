using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject, IUSEAction
    {
        public enum rarity_type { common, rare, very_rare, super_rare }

        public int item_id => GetInstanceID();
        [SerializeField]
        public Sprite icon;
        [SerializeField]
        public string item_name;
        [SerializeField]
        public string item_type;
        [SerializeField]
        public bool IsStackable;
        [SerializeField]
        public int max_stack = 1;
        [SerializeField]
        public rarity_type rarity;
        [SerializeField]
        public float weight;
        [field: TextArea]
        public string description;


        public bool showInventory = true;

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

        public bool UseAction(GameObject character, int quantity, List<ItemParameter> itemState)
        {
            InventoryController ItemSystem = character.GetComponent<InventoryController>();
            if (ItemSystem != null)
            {
                ItemSystem.UseItem(this, quantity, itemState == null ?
                    DefaultParametersList : itemState);
                return true;
            }
            return false;
        }

        public bool NotUseAction(GameObject character, int quantity, List<ItemParameter> itemState)
        {
            InventoryController ItemSystem = character.GetComponent<InventoryController>();
            if (ItemSystem != null)
            {
                ItemSystem.NotUseItem(this, quantity, itemState == null ?
                    DefaultParametersList : itemState);
                return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
