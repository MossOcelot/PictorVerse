using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UI_characterEquipment : MonoBehaviour
{
    [SerializeField] 
    private InventorySO InventoryData;
    [SerializeField]
    private InventorySO WeaponBox;

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

        Load();
    }

    Item capItem;
    List<ItemParameter> capDefaultParametersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs cap_e;
    private void capSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if(e.item.item_type == "cap")
        {
            cap_e = e;

            capItem = e.item;
            capDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetCap(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            capSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            capSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            capSlot_box.OnRightMouseBtnClick += HandleShowCapAction;

            CheckParameter(e.item, "increase");
        } else
        {
            Debug.Log("No cap");
        }
    }

    private void HandleShowCapAction(UIInventoryItem inventoryItemUI)
    {
        characterSetEquipment.RemoveCap(capItem, capDefaultParametersList);
        capSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);

        CheckParameter(capItem, "decrease");
    }

    Item bagItem;
    List<ItemParameter> bagDefaultParametersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs bag_e;
    private void bagSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "bag")
        {
            bag_e = e;

            bagItem = e.item;
            bagDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetBag(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            bagSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            bagSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            bagSlot_box.OnRightMouseBtnClick += HandleShowBagAction;

            CheckParameter(e.item, "increase");
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

        CheckParameter(bagItem, "decrease");
    }

    Item leggingsItem;
    List<ItemParameter> leggingsDefaultParametersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs leggings_e;

    private void leggingsSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "leggings")
        {
            leggings_e = e;

            leggingsItem = e.item;
            leggingsDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetLeggings(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            leggingsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            leggingsSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            leggingsSlot_box.OnRightMouseBtnClick += HandleShowLeggingsAction;

            CheckParameter(e.item, "increase");
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

        CheckParameter(leggingsItem, "decrease");
    }

    Item chestplateItem;
    List<ItemParameter> chestplateDefaultParametersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs chestplate_e;
    private void chestplateSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "chestplate")
        {
            chestplate_e = e;

            Debug.LogError("A");
            chestplateItem = e.item;
            chestplateDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetChestplate(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);
            chestplateSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            chestplateSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            chestplateSlot_box.OnRightMouseBtnClick += HandleShowChestplateAction;
            CheckParameter(e.item, "increase");
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

        CheckParameter(chestplateItem, "decrease");
    }

    Item bootsSlotItem;
    List<ItemParameter> bootsSlotDefaultParametersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs boots_e;

    private void bootsSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "boots")
        {
            boots_e = e;

            bootsSlotItem = e.item;
            bootsSlotDefaultParametersList = e.item.DefaultParametersList;
            characterSetEquipment.SetBoots(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            bootsSlot.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            bootsSlot_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            bootsSlot_box.OnRightMouseBtnClick += HandleShowBootsSlotAction;

            CheckParameter(e.item, "increase");
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
        CheckParameter(bootsSlotItem, "decrease");
    }

    Item weaponItem;
    List<ItemParameter> weaponDefaultParamtersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs weapon_e;

    private void weaponSlot1_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item dropped in Cap Slot
        if(e.item.item_type == "weapon")
        {
            weapon_e = e;

            weaponItem = e.item;
            weaponDefaultParamtersList = e.item.DefaultParametersList;
            characterWeaponEquipment.SetWeapon(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            weaponSlot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            weaponSlot1_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            weaponSlot1_box.OnRightMouseBtnClick += HandleShowItemActions;

            CheckParameter(e.item, "increase");
        } else
        {
            Debug.Log("No Weapon");
        }
    }


    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        characterWeaponEquipment.RemoveWeapon(weaponItem, weaponDefaultParamtersList);
        weaponSlot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);

        CheckParameter(weaponItem, "decrease");
    }

    Item weaponItem2;
    List<ItemParameter> weapon2DefaultParamtersList;
    public UI_CharacterEquipmentSlot.OnItemDroppedEventArgs shield_e;
    private void WeaponSlot2_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.item_type == "shield")
        {
            shield_e = e;

            weaponItem2 = e.item;
            weapon2DefaultParamtersList = e.item.DefaultParametersList;

            characterWeaponEquipment.SetWeapon2(e.item, e.item.DefaultParametersList);
            InventoryData.RemoveItem(e.indexSlot, 1);

            weaponSlot2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            weaponSlot2_box.SetData(e.indexSlot, e.item, e.item.icon, 1);
            weaponSlot2_box.OnRightMouseBtnClick += HandleShowItemActions2;

            CheckParameter(e.item, "increase");
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

        CheckParameter(weaponItem2, "decrease");
    }

    private void CheckParameter(Item item, string command)
    {
        List<ItemParameter> itemParameters = item.DefaultParametersList;

        foreach(ItemParameter parameter in itemParameters)
        {
            
            string parameterName = parameter.itemParameter.ParameterName;
            int value = (int)parameter.value;
            if (command == "increase")
            {
                ParameterActive(parameterName, value);
            }
            else if(command == "decrease")
            {
                ParameterActive(parameterName, -value);
            }
        }
    }

    private void ParameterActive(string parameterName, int value) 
    {
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
        if (parameterName == "HP")
        {
            playerStatus.setMaxHP(value);
            playerStatus.setHP(value);
        } else if(parameterName == "Strength")
        {
            playerMovement.strength += value;
        } else if(parameterName == "MoveSpeed")
        {
            playerMovement.moveSpeed += value;
        } else if(parameterName == "energyUse")
        {
            playerMovement.energy_for_walk -= value;
        }
    }


    private void OnApplicationQuit()
    {
        Save();
    }

    // // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerEquipment(this);
    }

    public void Load()
    {
        PlayerEquipmentData data = SavePlayerSystem.LoadPlayerEquipment();

        if (data != null)
        {
            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs weapon = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.weapon_index,
                item = data.weapon_item
            };

            weapon_e = weapon;
            weaponItem = data.weapon_item;
            weaponDefaultParamtersList = data.weapon_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs weapon2 = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.shield_index,
                item = data.shield_item
            };

            shield_e = weapon2;
            weaponItem2 = data.shield_item;
            weapon2DefaultParamtersList = data.shield_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs cap = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.cap_index,
                item = data.cap_item
            };

            cap_e = cap;
            capItem = data.cap_item;
            capDefaultParametersList = data.cap_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs bag = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.bag_index,
                item = data.bag_item
            };

            bag_e = bag;
            bagItem = data.bag_item;
            bagDefaultParametersList = data.bag_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs leggings = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.leggings_index,
                item = data.leggings_item
            };

            leggings_e = leggings;
            leggingsItem = data.leggings_item;
            leggingsDefaultParametersList = data.leggings_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs chestplate = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.chestplate_index,
                item = data.chestplate_item
            };

            chestplate_e = chestplate;
            chestplateItem = data.chestplate_item;
            chestplateDefaultParametersList = data.chestplate_item.DefaultParametersList;

            UI_CharacterEquipmentSlot.OnItemDroppedEventArgs boots = new UI_CharacterEquipmentSlot.OnItemDroppedEventArgs
            {
                indexSlot = data.boots_index,
                item = data.boots_item
            };

            boots_e = boots;
            bootsSlotItem = data.boots_item;
            bootsSlotDefaultParametersList = data.boots_item.DefaultParametersList;
        }
    }
}
