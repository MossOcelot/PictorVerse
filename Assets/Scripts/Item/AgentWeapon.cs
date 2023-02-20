using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItem weapon;
    [SerializeField]
    private InventorySO WeaponBox;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField] 
    private InventorySO miniInventoryData;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetWeapon(EquippableItem weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
            WeaponBox.RemoveItem(0, 1);
        }

        this.weapon = weaponItemSO;
        WeaponBox.AddItem(weapon, 1, itemCurrentState);
        this.itemCurrentState = new List<ItemParameter>(itemState);
        
        ModifyParameters();
    }

    public void RemoveWeapon(EquippableItem weaponItemSO, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(weapon, 1, itemCurrentState);
        WeaponBox.RemoveItem(0, 1);
        this.weapon = null;
        this.itemCurrentState = null;
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
}
