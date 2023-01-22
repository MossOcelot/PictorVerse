using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
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
        ExtraInventory.instance.Remove(this);
    }
}
