using inventory.Model;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UICraftingManager : MonoBehaviour
{
    public UIInventoryPage inventoryPage;

    [SerializeField]
    private InventorySO inventoryBag;
    public List<InventoryItem> initialItems = new List<InventoryItem>();
    [SerializeField] 
    private List<CraftingRecipe> craftingRecipes;

    [SerializeField]
    private InventorySO CraftingInventory;

    [SerializeField]
    private Transform craftTrans;
    [SerializeField]
    private Transform itemPanelContent;

    public GameObject ItemPanelCraftTemplate;
    GameObject itemPanelCraft;
    //
    private UI_CharacterEquipmentSlot slot1;
    private UIInventoryItem slot1_box;
    private UI_CharacterEquipmentSlot slot2;
    private UIInventoryItem slot2_box;
    private UI_CharacterEquipmentSlot slot3;
    private UIInventoryItem slot3_box;
    private UI_CharacterEquipmentSlot slot4;
    private UIInventoryItem slot4_box;
    private UI_CharacterEquipmentSlot slot5;
    private UIInventoryItem slot5_box;
    private UI_CharacterEquipmentSlot slot6;
    private UIInventoryItem slot6_box;
    private UI_CharacterEquipmentSlot slot7;
    private UIInventoryItem slot7_box;
    private UI_CharacterEquipmentSlot slot8;
    private UIInventoryItem slot8_box;
    private UI_CharacterEquipmentSlot slot9;
    private UIInventoryItem slot9_box;

    private void Start()
    {
        PrepareInventoryUI();
        PrepareInventoryData();
    }

    private void Awake()
    {
        slot1 = craftTrans.GetChild(0).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot1_box = craftTrans.GetChild(0).gameObject.GetComponent<UIInventoryItem>();

        slot2 = craftTrans.GetChild(1).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot2_box = craftTrans.GetChild(1).gameObject.GetComponent<UIInventoryItem>();

        slot3 = craftTrans.GetChild(2).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot3_box = craftTrans.GetChild(2).gameObject.GetComponent<UIInventoryItem>();

        slot4 = craftTrans.GetChild(3).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot4_box = craftTrans.GetChild(3).gameObject.GetComponent<UIInventoryItem>();

        slot5 = craftTrans.GetChild(4).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot5_box = craftTrans.GetChild(4).gameObject.GetComponent<UIInventoryItem>();

        slot6 = craftTrans.GetChild(5).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot6_box = craftTrans.GetChild(5).gameObject.GetComponent<UIInventoryItem>();

        slot7 = craftTrans.GetChild(6).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot7_box = craftTrans.GetChild(6).gameObject.GetComponent<UIInventoryItem>();

        slot8 = craftTrans.GetChild(7).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot8_box = craftTrans.GetChild(7).gameObject.GetComponent<UIInventoryItem>();

        slot9 = craftTrans.GetChild(8).gameObject.GetComponent<UI_CharacterEquipmentSlot>();
        slot9_box = craftTrans.GetChild(8).gameObject.GetComponent<UIInventoryItem>();

        slot1.OnItemDropped += slot1_OnItemDropped;
        slot2.OnItemDropped += slot2_OnItemDropped;
        slot3.OnItemDropped += slot3_OnItemDropped;
        slot4.OnItemDropped += slot4_OnItemDropped;
        slot5.OnItemDropped += slot5_OnItemDropped;
        slot6.OnItemDropped += slot6_OnItemDropped;
        slot7.OnItemDropped += slot7_OnItemDropped;
        slot8.OnItemDropped += slot8_OnItemDropped;
        slot9.OnItemDropped += slot9_OnItemDropped;
    }

    private void Update()
    {
        // open close inventoryPage
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (inventoryPage.isActiveAndEnabled == false)
            {

                inventoryPage.show();
                foreach (var item in inventoryBag.GetCurrentInventoryState())
                {
                    inventoryPage.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
                }
            } else
            {
                int len = CraftingInventory.GetCurrentInventoryState().Count;
                if(len > 0)
                {
                    foreach(int index in CraftingInventory.GetCurrentInventoryState().Keys) 
                    {
                        InventoryItem item = CraftingInventory.GetCurrentInventoryState()[index];
                        inventoryBag.AddItem(item);
                        CraftingInventory.RemoveItem(index, item.quantity);
                    }

                }
                clearAllCraftSlot();
                inventoryPage.hide();
            }
        }
    }

    public void clearAllCraftSlot()
    {
        int len = craftTrans.childCount;
        for(int i = 0; i < len; i++)
        {
            InventoryItem item = CraftingInventory.getInventoryItems()[i];
            if(item.item == null)
            {
                craftTrans.GetChild(i).gameObject.GetComponent<UIInventoryItem>().ResetData();
                continue;
            }
            int index = craftTrans.GetChild(i).gameObject.GetComponent<UIInventoryItem>().GetIndex();
            craftTrans.GetChild(i).gameObject.GetComponent<UIInventoryItem>().SetData(index, item.item, item.item.icon, item.quantity);
        }
    }
    public List<CraftingRecipe> have_resive;
    private void FixedUpdate()
    {
        foreach(CraftingRecipe recipe in craftingRecipes)
        {
            bool status = recipe.CanCraft(CraftingInventory);

            if(status)
            {
                bool IsHave = have_resive.Contains(recipe);
                if (IsHave) continue;
                have_resive.Add(recipe);
                
                foreach (InventoryItem result in recipe.Results)
                {
                    itemPanelCraft = Instantiate(ItemPanelCraftTemplate, itemPanelContent);
                    ItemPanelCraft itemPanel = itemPanelCraft.gameObject.GetComponent<ItemPanelCraft>();
                    itemPanel.SetData(result.item.icon, result.item.item_name, 1);
                    
                    itemPanelCraft.gameObject.GetComponent<Button>().AddEventListener(recipe, Crafting);
                    
                }
            } else
            {
                bool IsHave = have_resive.Contains(recipe);
                if (IsHave)
                {
                    int len = itemPanelContent.childCount;
                    for(int i = 0; i < len; i++)
                    {
                        Destroy(itemPanelContent.GetChild(i).gameObject);
                    }
                    have_resive.Remove(recipe);
                }
            }
        }
    }

    private void Crafting(CraftingRecipe recipe)
    {
        recipe.Craft(CraftingInventory, inventoryBag);
        clearAllCraftSlot();
        have_resive.Clear();
        int len = itemPanelContent.childCount;
        for (int i = 0; i < len; i++)
        {
            Destroy(itemPanelContent.GetChild(i).gameObject);
        }
    }

    private void slot1_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot1_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(0, e.item, e.item_amount);
        slot1_box.OnRightMouseBtnClick += HandleShowSlot1Action;
    }

    private void HandleShowSlot1Action(UIInventoryItem inventoryItemUI)
    {
        int index = 0;
        int amount = inventoryItemUI.GetItemAmount();   
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot2_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot2_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(1, e.item, e.item_amount);
        slot2_box.OnRightMouseBtnClick += HandleShowSlot2Action;
    }
    private void HandleShowSlot2Action(UIInventoryItem inventoryItemUI)
    {
        int index = 1;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot3_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot3.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot3_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(2, e.item, e.item_amount);
        slot3_box.OnRightMouseBtnClick += HandleShowSlot3Action;
    }
    private void HandleShowSlot3Action(UIInventoryItem inventoryItemUI)
    {
        int index = 2;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot3.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot4_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot4.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot4_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(3, e.item, e.item_amount);
        slot4_box.OnRightMouseBtnClick += HandleShowSlot4Action;
    }
    private void HandleShowSlot4Action(UIInventoryItem inventoryItemUI)
    {
        int index = 3;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot4.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot5_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot5.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot5_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(4, e.item, e.item_amount);
        slot5_box.OnRightMouseBtnClick += HandleShowSlot5Action;
    }
    private void HandleShowSlot5Action(UIInventoryItem inventoryItemUI)
    {
        int index = 4;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot5.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot6_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot6.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot6_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(5, e.item, e.item_amount);
        slot6_box.OnRightMouseBtnClick += HandleShowSlot6Action;
    }
    private void HandleShowSlot6Action(UIInventoryItem inventoryItemUI)
    {
        int index = 5;
        int amount = inventoryItemUI.GetItemAmount();   
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot6.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot7_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot7.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot7_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(6, e.item, e.item_amount);
        slot7_box.OnRightMouseBtnClick += HandleShowSlot7Action;
    }
    private void HandleShowSlot7Action(UIInventoryItem inventoryItemUI)
    {
        int index = 6;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot7.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void slot8_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot8.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot8_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(7, e.item, e.item_amount);
        slot8_box.OnRightMouseBtnClick += HandleShowSlot8Action;
    }
    private void HandleShowSlot8Action(UIInventoryItem inventoryItemUI)
    {
        int index = 7;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot8.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void slot9_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.name == "craftSlot") return;
        slot9.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        slot9_box.SetData(e.indexSlot, e.item, e.item.icon, e.item_amount);
        inventoryBag.RemoveItem(e.indexSlot, e.item_amount);
        CraftingInventory.AddItemInIndex(8, e.item, e.item_amount);
        slot9_box.OnRightMouseBtnClick += HandleShowSlot9Action;
    }
    private void HandleShowSlot9Action(UIInventoryItem inventoryItemUI)
    {
        int index = 8;
        int amount = inventoryItemUI.GetItemAmount();
        CraftingInventory.RemoveItem(index, amount);

        inventoryBag.AddItem(inventoryItemUI.GetItem(), amount);
        inventoryItemUI.ResetData();
        slot9.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    private void PrepareInventoryUI()
    {
        inventoryPage.InitializeInventoryUI(inventoryBag.Size);
        inventoryPage.OnSwapItems += HandleSwapItems;
        inventoryPage.OnStartDragging += HandleDragging;
        inventoryPage.OnDescriptionRequested += HandleItemDescriptionRequest;
    }

    private void PrepareInventoryData()
    {
        inventoryBag.Initialize();
        inventoryBag.OnInventoryUpdated += UpdateInventoryUI;
        foreach (var item in initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryBag.AddItem(item);
        }
    }
    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryPage.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryPage.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);

        }
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryBag.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryBag.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            return;
        }
        inventoryPage.CreateDraggedItem(itemIndex, inventoryItem.item, inventoryItem.item.icon, inventoryItem.quantity);
    }

    public void HandleItemDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryBag.GetItemAt(itemIndex);
        // inventoryUI
        if (inventoryItem.IsEmpty)
        {
            inventoryPage.hideItem(itemIndex);
            return;
        }

        //ddata
        string item_name = inventoryItem.item.item_name;
        int item_quantity = inventoryItem.quantity;
        float item_price = inventoryItem.price;
        string item_description = inventoryItem.item.description;

        inventoryPage.showItemDescriptionAction(itemIndex);
        inventoryPage.AddDescription(item_name, item_quantity, item_price, item_description);

        IItemAction itemAction = inventoryItem.item as IItemAction;
        int n = 0;
        if (itemAction != null)
        {
            inventoryPage.AddActionInDescription(n, itemAction.ActionName, () => PerformAction(itemIndex));
            n++;
        }

        IUSEAction itemUseAction = inventoryItem.item as IUSEAction;
        if (itemUseAction != null)
        {
            inventoryPage.AddActionInDescription(n, "use", () => UseAction(itemIndex));
            n++;
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryPage.AddActionInDescription(n, "Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }
    }

    public void PerformAction(int itemIndex)
    {
        PlayerStatus playerStatus = gameObject.GetComponent<PlayerStatus>();
        if (playerStatus.getHP() >= playerStatus.getMaxHP() && playerStatus.getEnergy() >= playerStatus.getMaxEnergy()) return;
        InventoryItem inventoryItem = inventoryBag.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryBag.RemoveItem(itemIndex, 1);
        }
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryBag.GetItemAt(itemIndex).IsEmpty)
                inventoryPage.ResetSelection();
        }
    }

    public void UseAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryBag.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryBag.RemoveItem(itemIndex, inventoryItem.quantity);
        }

        IUSEAction itemAction = inventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            itemAction.UseAction(gameObject, inventoryItem.quantity, inventoryItem.itemState);

            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryBag.GetItemAt(itemIndex).IsEmpty)
                inventoryPage.ResetSelection();
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryBag.RemoveItem(itemIndex, quantity);
        inventoryPage.ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }
}
