using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/InventorySO")]
class InventorySO : ScriptableObject
{
    //  
}

[System.Serializable]
public struct InventoryItem
{
    public int quantity;
    public Item item;
    public bool IsEmpty => item == null;
    public string owner;
    public string before_owner;
    public int price;
    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity,
            owner = this.owner,
            before_owner = this.before_owner,
            price = this.price  
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
            price = this.price
        };
    }

    public InventoryItem ChangePrice(int newPrice)
    {
        return new InventoryItem {
            item = this.item,
            quantity = this.quantity,
            owner = this.owner,
            before_owner = this.owner,
            price = newPrice
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
            price = 0
        };
    }

}

