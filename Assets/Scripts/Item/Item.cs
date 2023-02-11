using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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


        // use item function
        public void Use()
        {

        }
        //public void RemoveItemFromInventory()
        //{
        //   Inventory.instance.Remove(this);
        //}

        public void RemoveItemFromExtraInventory()
        {
            //ExtraInventory.instance.Remove(this);
        }
    }
}
