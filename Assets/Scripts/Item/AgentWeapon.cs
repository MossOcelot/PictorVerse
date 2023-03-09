using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private Item weapon1;
    [SerializeField]
    private Item weapon2;
    [SerializeField]
    private InventorySO WeaponBox;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField] 
    private InventorySO miniInventoryData;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;
    [SerializeField]
    private List<ItemParameter> parametersToModify2, itemCurrentState2;

    public Item GetWeaponItem()
    {
        return weapon1;
    }

    public Item GetWeaponItem2()
    {
        return weapon2;
    }

    public List<ItemParameter> GetItemCurrentState()
    {
        return itemCurrentState;
    }

    public List<ItemParameter> GetItemCurrentState2()
    {
        return itemCurrentState2;
    }

    public void SetWeapon(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon1 != null)
        {
            inventoryData.AddItem(weapon1, 1, itemCurrentState);
            WeaponBox.RemoveItem(0, 1);
        }

        this.weapon1 = weaponItemSO;
        WeaponBox.AddItemInIndex(0, weapon1, 1, itemCurrentState);
        this.itemCurrentState = new List<ItemParameter>(itemState);
        
        ModifyParameters();
    }

    public void RemoveWeapon(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(weapon1, 1, itemCurrentState);
        WeaponBox.RemoveItem(0, 1);
        this.weapon1 = null;
        this.itemCurrentState = null;
    }

    public void SetWeapon2(Item weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon2 != null)
        {
            inventoryData.AddItem(weapon2, 1, itemCurrentState2);
            WeaponBox.RemoveItem(1, 1);
        }

        this.weapon2 = weaponItemSO;
        WeaponBox.AddItemInIndex(1, weapon2, 1, itemCurrentState2);
        this.itemCurrentState2 = new List<ItemParameter>(itemState);

        ModifyParameters2();
    }

    public void RemoveWeapon2(Item weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(weapon2, 1, itemCurrentState2);
        WeaponBox.RemoveItem(1, 1);
        this.weapon2 = null;
        this.itemCurrentState2 = null;
    }

    // เลือกเข้า miniInventory
    public void UseWeapon(EquippableItem weaponItemSO, List<ItemParameter> itemState)
    {
        miniInventoryData.AddItem(weaponItemSO, 1, itemCurrentState);
    }

    public void NotUseWeapon(EquippableItem weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(weaponItemSO, 1, itemCurrentState);
    }
    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }

    private void ModifyParameters2()
    {
        foreach (var parameter in parametersToModify2)
        {
            if (itemCurrentState2.Contains(parameter))
            {
                int index = itemCurrentState2.IndexOf(parameter);
                float newValue = itemCurrentState2[index].value + parameter.value;
                itemCurrentState2[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }

    private void Awake()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerAgentWeapon(this);
    }

    public void Load()
    {
        PlayerAgentWeaponData data = SavePlayerSystem.LoadPlayerAgentWeapon();

        if (data != null)
        {
            weapon1 = data.weapon1; 
            weapon2 = data.weapon2;
            itemCurrentState = data.itemCurrentState;
            itemCurrentState2 = data.itemCurrentState2;
        }
    }
}
