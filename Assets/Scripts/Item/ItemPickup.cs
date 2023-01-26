using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractiveObject
{
    public Item item;
    public override void Interact()
    {
        Debug.Log("pick up");
        base.Interact();
        pickUp();


    }
    void pickUp()
    {
        ExtraInventory.instance.Add(item);
        gameObject.SetActive(false);
    }
}
