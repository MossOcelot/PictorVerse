using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
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
