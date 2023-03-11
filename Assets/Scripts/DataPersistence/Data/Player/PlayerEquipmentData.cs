using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerEquipmentData
{
    public int weapon_index;
    public Item weapon_item;

    public int shield_index;
    public Item shield_item;

    public int cap_index;
    public Item cap_item;

    public int bag_index;
    public Item bag_item;   

    public int leggings_index;
    public Item leggings_item;

    public int chestplate_index;
    public Item chestplate_item;

    public int boots_index;
    public Item boots_item;


    public PlayerEquipmentData(UI_characterEquipment ui_characterEquipment)
    {
        weapon_index = ui_characterEquipment.weapon_e.indexSlot;
        weapon_item = ui_characterEquipment.weapon_e.item;
        shield_index = ui_characterEquipment.shield_e.indexSlot;
        shield_item = ui_characterEquipment.shield_e.item;

        cap_index = ui_characterEquipment.cap_e.indexSlot;
        cap_item = ui_characterEquipment.cap_e.item;

        bag_index = ui_characterEquipment.bag_e.indexSlot;
        bag_item = ui_characterEquipment.bag_e.item;

        leggings_index = ui_characterEquipment.leggings_e.indexSlot;
        leggings_item = ui_characterEquipment.leggings_e.item;

        chestplate_index = ui_characterEquipment.leggings_e.indexSlot;
        chestplate_item = ui_characterEquipment.leggings_e.item;

        boots_index = ui_characterEquipment.leggings_e.indexSlot;
        boots_item = ui_characterEquipment.leggings_e.item;

}
}
