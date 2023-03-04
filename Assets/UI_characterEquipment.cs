using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_characterEquipment : MonoBehaviour
{
    [SerializeField] 
    private InventorySO InventoryData;
    [SerializeField]
    private InventorySO WeaponBox;

    private Transform itemContainer;
    private UI_CharacterEquipmentSlot capSlot;
    private UIInventoryItem capSlot_box;
    private UI_CharacterEquipmentSlot bagSlot;
    private UIInventoryItem bagSlot_box;
    private UI_CharacterEquipmentSlot chestplateSlot;
    private UIInventoryItem chestplateSlot_box;
    private UI_CharacterEquipmentSlot leggingsSlot;
    private UIInventoryItem leggingsSlot_box;
    private UI_CharacterEquipmentSlot bootsSlot;
    private UIInventoryItem bootsSlot_box;
    private UI_CharacterEquipmentSlot weaponSlot1;
    private UIInventoryItem weaponSlot1_box;
    private UI_CharacterEquipmentSlot weaponSlot2;
    private UIInventoryItem weaponSlot2_box;
    [SerializeField]
    private AgentWeapon characterWeaponEquipment;
    [SerializeField]
    private AgentSet characterSetEquipment;

    private void Awake()
    {
        itemContainer = transform.Find("itemContainer");
        capSlot = transform.Find("capSlot").GetComponent<UI_CharacterEquipmentSlot>();  
        capSlot_box = transform.Find("capSlot").GetComponent<UIInventoryItem>();

        bagSlot = transform.Find("bagSlot").GetComponent<UI_CharacterEquipmentSlot>();
        bagSlot_box = transform.Find("bagSlot").GetComponent<UIInventoryItem>();

        chestplateSlot = transform.Find("chestplateSlot").GetComponent<UI_CharacterEquipmentSlot>();
        chestplateSlot_box = transform.Find("chestplateSlot").GetComponent<UIInventoryItem>();

        leggingsSlot = transform.Find("leggingsSlot").GetComponent<UI_CharacterEquipmentSlot>();
        leggingsSlot_box = transform.Find("leggingsSlot").GetComponent<UIInventoryItem>();

        bootsSlot = transform.Find("bootsSlot").GetComponent<UI_CharacterEquipmentSlot>();
        bootsSlot_box = transform.Find("bootsSlot").GetComponent<UIInventoryItem>();

        weaponSlot1 = transform.Find("weaponSlot1").GetComponent<UI_CharacterEquipmentSlot>();
        weaponSlot1_box = transform.Find("weaponSlot1").GetComponent<UIInventoryItem>();
        
        weaponSlot2 = transform.Find("weaponSlot2").GetComponent<UI_CharacterEquipmentSlot>();
        weaponSlot2_box = transform.Find("weaponSlot2").GetComponent<UIInventoryItem>();

        capSlot.OnItemDropped += capSlot_OnItemDropped;
        bagSlot.OnItemDropped += bagSlot_OnItemDropped;
        chestplateSlot.OnItemDropped += chestplateSlot_OnItemDropped;
        leggingsSlot.OnItemDropped += leggingsSlot_OnItemDropped;
        bootsSlot.OnItemDropped += bootsSlot_OnItemDropped;
        weaponSlot1.OnItemDropped += weaponSlot1_OnItemDropped;
        weaponSlot2.OnItemDropped += WeaponSlot2_OnItemDropped;
    }

    Item capItem;
    List<ItemParameter> capDefaultParametersList;

    private void capSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if(e.item.item_type == "cap")
        {
            capItem = e.item;
            capDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetCap(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            capSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            capSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            capSlot_box.OnRightMouseBtnClick += HandleShowCapAction;
        } else
        {
            Debug.Log("No cap");
        }
    }

    private void HandleShowCapAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveCap(capItem, capDefaultParametersList);
        capSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item bagItem;
    List<ItemParameter> bagDefaultParametersList;

    private void bagSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "bag")
        {
            bagItem = e.item;
            bagDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetBag(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            bagSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            bagSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            bagSlot_box.OnRightMouseBtnClick += HandleShowBagAction;
        }
        else
        {
            Debug.Log("No bag");
        }
    }

    private void HandleShowBagAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveBag(bagItem, bagDefaultParametersList);
        bagSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item leggingsItem;
    List<ItemParameter> leggingsDefaultParametersList;

    private void leggingsSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "leggings")
        {
            leggingsItem = e.item;
            leggingsDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetLeggings(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            leggingsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            leggingsSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            leggingsSlot_box.OnRightMouseBtnClick += HandleShowLeggingsAction;
        }
        else
        {
            Debug.Log("No leggings");
        }
    }

    private void HandleShowLeggingsAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveLeggings(leggingsItem, leggingsDefaultParametersList);
        leggingsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item chestplateItem;
    List<ItemParameter> chestplateDefaultParametersList;

    private void chestplateSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "chestplate")
        {
            chestplateItem = e.item;
            chestplateDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetChestplate(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            chestplateSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            chestplateSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            chestplateSlot_box.OnRightMouseBtnClick += HandleShowChestplateAction;
        }
        else
        {
            Debug.Log("No chestplate");
        }
    }

    private void HandleShowChestplateAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveChestplate(chestplateItem, chestplateDefaultParametersList);
        chestplateSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item bootsSlotItem;
    List<ItemParameter> bootsSlotDefaultParametersList;

    private void bootsSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "boots")
        {
            bootsSlotItem = e.item;
            bootsSlotDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetBoots(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            bootsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            bootsSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            bootsSlot_box.OnRightMouseBtnClick += HandleShowBootsSlotAction;
        }
        else
        {
            Debug.Log("No boots");
        }
    }

    private void HandleShowBootsSlotAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveBoots(bootsSlotItem, bootsSlotDefaultParametersList);
        bootsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item weaponItem;
    List<ItemParameter> weaponDefaultParamtersList;

    private void weaponSlot1_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item dropped in Cap Slot
        if(e.item.item_type == "weapon")
        {
            weaponItem = e.item;
            weaponDefaultParamtersList = e.item.DefaultParametersList;
            characterWeaponEquipment.SetWeapon(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            weaponSlot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            weaponSlot1_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            weaponSlot1_box.OnRightMouseBtnClick += HandleShowItemActions;
        } else
        {
            Debug.Log("No Weapon");
        }
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        characterWeaponEquipment.RemoveWeapon(weaponItem, weaponDefaultParamtersList);
        weaponSlot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    Item weaponItem2;
    List<ItemParameter> weapon2DefaultParamtersList;
    private void WeaponSlot2_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "shield")
        {
            weaponItem2 = e.item;
            weapon2DefaultParamtersList = e.item.DefaultParametersList;

            characterWeaponEquipment.SetWeapon2(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            weaponSlot2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            weaponSlot2_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            weaponSlot2_box.OnRightMouseBtnClick += HandleShowItemActions2;
        }
        else
        {
            Debug.Log("No Weapon");
        }
    }

    private void HandleShowItemActions2(UIInventoryItem inventoryItemUI)
    {
        characterWeaponEquipment.RemoveWeapon2(weaponItem2, weapon2DefaultParamtersList);
        weaponSlot2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

}
