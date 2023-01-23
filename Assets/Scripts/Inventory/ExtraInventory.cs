using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInventory : MonoBehaviour
{
    public int space = 10;
    public List<Item> items = new List<Item>();
    public static ExtraInventory instance;
    private void Awake()
    {
        instance = this;
    }
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Add(Item item)
    {
        if (item.showInventory)
        {
            if (items.Count >= space)
                Inventory.instance.items.Add(item);
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }


}
