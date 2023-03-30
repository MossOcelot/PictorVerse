using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSet : MonoBehaviour
{
    [SerializeField]
    private Item cap;
    [SerializeField]
    private Item bag;
    [SerializeField]
    private Item chestplate;
    [SerializeField]
    private Item leggings;
    [SerializeField]
    private Item boots;

    [SerializeField]
    private InventorySO SetBox;
    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify_cap, itemCurrentState_cap;
    [SerializeField]
    private List<ItemParameter> parametersToModify_bag, itemCurrentState_bag;
    [SerializeField]
    private List<ItemParameter> parametersToModify_chestplate, itemCurrentState_chestplate;
    [SerializeField]
    private List<ItemParameter> parametersToModify_leggings, itemCurrentState_leggings;
    [SerializeField]
    private List<ItemParameter> parametersToModify_boots, itemCurrentState_boots;

    public Item GetCap()
    {
        return cap;
    }

    public Item GetBag()
    {
        return bag;
    }

    public Item GetChestplate()
    {
        return chestplate;
    }

    public Item GetLeggings()
    {
        return leggings;
    }

    public Item GetBoots()
    {
        return boots;
    }
    public List<ItemParameter> GetItemCurrentState_cap()
    {
        return itemCurrentState_cap;
    }
    public List<ItemParameter> GetItemCurrentState_bag()
    {
        return itemCurrentState_bag;
    }
    public List<ItemParameter> GetItemCurrentState_chestplate()
    {
        return itemCurrentState_chestplate;
    }
    public List<ItemParameter> GetItemCurrentState_leggings()
    {
        return itemCurrentState_leggings;
    }
    public List<ItemParameter> GetItemCurrentState_boots()
    {
        return itemCurrentState_boots;
    }
    public void SetCap(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (cap != null)
        {
            inventoryData.AddItem(cap, 1, itemCurrentState_cap);
            SetBox.RemoveItem(0, 1);
        }

        this.cap = weaponItemSO;
        SetBox.AddItemInIndex(0, cap, 1, itemCurrentState_cap);
        this.itemCurrentState_cap = new List<ItemParameter>(itemState);

        // ModifyParameters();
    }

    public void RemoveCap(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(cap, 1, itemCurrentState_cap);
        SetBox.RemoveItem(0, 1);
        this.cap = null;
        this.itemCurrentState_cap = null;
    }

    public void SetBag(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (bag != null)
        {
            inventoryData.AddItem(bag, 1, itemCurrentState_bag);
            SetBox.RemoveItem(1, 1);
        }

        this.bag = weaponItemSO;
        SetBox.AddItemInIndex(1, bag, 1, itemCurrentState_bag);
        this.itemCurrentState_bag = new List<ItemParameter>(itemState);

        // ModifyParameters();
    }

    public void RemoveBag(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(bag, 1, itemCurrentState_bag);
        SetBox.RemoveItem(1, 1);
        this.bag = null;
        this.itemCurrentState_bag = null;
    }

    public void SetChestplate(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (chestplate != null)
        {
            inventoryData.AddItem(chestplate, 1, itemCurrentState_chestplate);
            SetBox.RemoveItem(2, 1);
        }

        this.chestplate = weaponItemSO;
        SetBox.AddItemInIndex(2, chestplate, 1, itemCurrentState_chestplate);
        this.itemCurrentState_chestplate = new List<ItemParameter>(itemState);

        // ModifyParameters();
    }

    public void RemoveChestplate(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(chestplate, 1, itemCurrentState_chestplate);
        SetBox.RemoveItem(2, 1);
        this.chestplate = null;
        this.itemCurrentState_chestplate = null;
    }

    public void SetLeggings(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (leggings != null)
        {
            inventoryData.AddItem(leggings, 1, itemCurrentState_leggings);
            SetBox.RemoveItem(3, 1);
        }

        this.leggings = weaponItemSO;
        SetBox.AddItemInIndex(3, leggings, 1, itemCurrentState_leggings);
        this.itemCurrentState_leggings = new List<ItemParameter>(itemState);

        // ModifyParameters();
    }

    public void RemoveLeggings(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(leggings, 1, itemCurrentState_leggings);
        SetBox.RemoveItem(3, 1);
        this.leggings = null;
        this.itemCurrentState_leggings = null;
    }

    public void SetBoots(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (boots != null)
        {
            inventoryData.AddItem(boots, 1, itemCurrentState_boots);
            SetBox.RemoveItem(4, 1);
        }
        this.boots = weaponItemSO;
        SetBox.AddItemInIndex(4, boots, 1, itemCurrentState_boots);
        this.itemCurrentState_boots = new List<ItemParameter>(itemState);

        // ModifyParameters();
    }

    public void RemoveBoots(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(boots, 1, itemCurrentState_boots);
        SetBox.RemoveItem(4, 1);
        this.boots = null;
        this.itemCurrentState_boots = null;
    }

    private void Awake()
    {
        // Load();
    }

    private void OnApplicationQuit()
    {
        // Save();
    }

    // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerAgentSet(this);
    }

    public void Load()
    {
        PlayerAgentSetData data = SavePlayerSystem.LoadPlayerAgentSet();

        if (data != null)
        {
            cap = data.cap;
            bag = data.bag;
            chestplate = data.chestplate;
            leggings = data.leggings;
            boots = data.boots;

            itemCurrentState_cap = data.itemCurrentState_cap;
            itemCurrentState_bag = data.itemCurrentState_bag;
            itemCurrentState_chestplate = data.itemCurrentState_chestplate;
            itemCurrentState_leggings = data.itemCurrentState_leggings;
            itemCurrentState_boots = data.itemCurrentState_boots;
        }
    }
}
