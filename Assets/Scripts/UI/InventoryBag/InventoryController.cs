using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static QuestObjective;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;
    [SerializeField]
    private UIWeaponBox WeaponBoxUI;
    [SerializeField]
    private UIMiniInventoryPage miniInventoryUI;
    [SerializeField]
    private UIBigMiniInventory bigMiniInventoryUI;
    [SerializeField]
    private Update_player_pocket Player_pocket;
    [SerializeField]
    private ExchangeManager Foreign_Exchange;


    public InventorySO inventoryData;
    public InventorySO WeaponBoxData;
    public InventorySO miniInventoryData;
    

    public List<InventoryItem> initialItems= new List<InventoryItem>();
    public List<InventoryItem> WeaponItems= new List<InventoryItem>();
    public List<InventoryItem> miniInitialItem= new List<InventoryItem>();

    public GameObject MyInventoryObj;
    public GameObject MyAccountObj;
    /*
    [SerializeField]
    private AudioClip dropClip;
    [SerializeField]
    private AudioSource audioSource;
    */
    public void Start()
    {
        PrepareInventoryUI();
        PrepareInventoryData();

        PrepareWeaponBoxData();

        PrepareMiniInventoryUI();
        PrepareMiniInventoryData();
    }

    //
    public void UseItem(Item item, int quantity, List<ItemParameter> itemState)
    {
        miniInventoryData.AddItem(item, quantity, itemState);
    }

    public void NotUseItem(Item item, int quantity, List<ItemParameter> itemState)
    {
        inventoryData.AddItem(item, quantity, itemState);
    }
    //

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (var item in initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryData.AddItem(item);
        }
    }

    private void PrepareWeaponBoxData()
    {
        WeaponBoxData.Initialize();
        WeaponBoxData.OnInventoryUpdated += UpdateWeaponBoxUI;
        foreach (var item in WeaponItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            WeaponBoxData.AddItem(item);
        }
    }

    private void PrepareMiniInventoryData()
    {
        miniInventoryData.Initialize();
        miniInventoryData.OnInventoryUpdated += UpdateminiInventoryUI;
        foreach (var item in miniInitialItem)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            miniInventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);

        }
    }

    private void UpdateWeaponBoxUI(Dictionary<int, InventoryItem> inventoryState)
    {
        WeaponBoxUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            WeaponBoxUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);

        }
    }

    public void UpdateminiInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        miniInventoryUI.ResetAllItems();
        bigMiniInventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            miniInventoryUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
            bigMiniInventoryUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
        }
    }

    private void PrepareInventoryUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnDescriptionRequested += HandleItemDescriptionRequest;
    }

    private void PrepareMiniInventoryUI()
    {
        miniInventoryUI.InitializeMiniInventoryUI(miniInventoryData.Size);
        this.miniInventoryUI.OnSwapItems += HandleMiniSwapItems;
        this.miniInventoryUI.OnStartDragging += HandleMiniDragging;
        this.miniInventoryUI.OnDescriptionRequested += HandleMiniItemDescriptionRequest;
    }

    private void HandleMiniDragging(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventoryData.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
        {
            return;
        }
        miniInventoryUI.CreateDraggedItem(itemIndex, miniInventoryItem.item, miniInventoryItem.item.icon, miniInventoryItem.quantity);
    }

    private void HandleMiniSwapItems(int itemIndex1, int itemIndex2)
    {
        miniInventoryData.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleMiniItemDescriptionRequest(int itemIndex)
    {
        InventoryItem MiniItem = miniInventoryData.GetItemAt(itemIndex);
        if (MiniItem.IsEmpty)
        {
            miniInventoryUI.hideItem(itemIndex);
            return;
        }

        //ddata
        string item_name = MiniItem.item.item_name;
        int item_quantity = MiniItem.quantity;
        float item_price = MiniItem.price;
        string item_description = MiniItem.item.description;

        miniInventoryUI.showItemDescriptionAction(itemIndex);
        miniInventoryUI.AddDescription(item_name, item_quantity, item_price, item_description);

        int n = 0;
        IItemAction itemAction = MiniItem.item as IItemAction;
        if (itemAction != null)
        { 
            miniInventoryUI.AddActionInDescription(n, itemAction.ActionName, () => PerformMiniAction(itemIndex));
            n++;
        }

        IUSEAction itemUSEAction = MiniItem.item as IUSEAction;
        if (itemUSEAction != null)
        {
            miniInventoryUI.AddActionInDescription(n, "Notuse", () => NotUseAction(itemIndex));
            n++;
        }

        IDestroyableItem destroyableItem = MiniItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventoryUI.AddActionInDescription(n, "Drop", () => DropItem(itemIndex, MiniItem.quantity));
        }
    }

    public void HandleItemDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        // inventoryUI
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.hideItem(itemIndex);
            return;
        }
        
        //ddata
        string item_name = inventoryItem.item.item_name;
        int item_quantity = inventoryItem.quantity;
        float item_price = inventoryItem.price;
        string item_description = inventoryItem.item.description;

        inventoryUI.showItemDescriptionAction(itemIndex);
        inventoryUI.AddDescription(item_name, item_quantity, item_price, item_description);

        IItemAction itemAction = inventoryItem.item as IItemAction;
        int n = 0;
        if (itemAction != null)
        {
            inventoryUI.AddActionInDescription(n, itemAction.ActionName, () => PerformAction(itemIndex));
            n++;
        }

        IUSEAction itemUseAction = inventoryItem.item as IUSEAction;
        if (itemUseAction != null)
        {
            inventoryUI.AddActionInDescription(n, "use", () => UseAction(itemIndex));
            n++;
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddActionInDescription(n, "Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }

    public void PerformMiniAction(int itemIndex)
    {
        PlayerStatus playerStatus = gameObject.GetComponent<PlayerStatus>();
        if (playerStatus.getHP() >= playerStatus.getMaxHP() && playerStatus.getEnergy() >= playerStatus.getMaxEnergy()) return;
        InventoryItem inventoryItem = miniInventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventoryData.RemoveItem(itemIndex, 1);
        }
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (miniInventoryData.GetItemAt(itemIndex).IsEmpty)
                miniInventoryUI.ResetSelection();
        }
    }
    public void PerformAction(int itemIndex)
    {
        PlayerStatus playerStatus = gameObject.GetComponent<PlayerStatus>();
        if (playerStatus.getHP() >= playerStatus.getMaxHP() && playerStatus.getEnergy() >= playerStatus.getMaxEnergy()) return;
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
    }

    public void NoperformAction(int itemIndex)
    {
        InventoryItem inventoryItem = WeaponBoxData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            WeaponBoxData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.NoperformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (WeaponBoxData.GetItemAt(itemIndex).IsEmpty)
                WeaponBoxUI.ResetSelection();
        }
    }

    public void UseAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, inventoryItem.quantity);
        }

        IUSEAction itemAction = inventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            itemAction.UseAction(gameObject, inventoryItem.quantity, inventoryItem.itemState);
            
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
    }

    public void NotUseAction(int itemIndex)
    {
        InventoryItem miniInventoryItem = miniInventoryData.GetItemAt(itemIndex);
        if (miniInventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = miniInventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            miniInventoryData.RemoveItem(itemIndex, miniInventoryItem.quantity);
        }

        IUSEAction itemAction = miniInventoryItem.item as IUSEAction;
        if (itemAction != null)
        {
            itemAction.NotUseAction(gameObject, miniInventoryItem.quantity, miniInventoryItem.itemState);
            
            
            //audioSource.PlayOneShot(itemAction.actionSFX
            if (miniInventoryData.GetItemAt(itemIndex).IsEmpty)
                miniInventoryUI.ResetSelection();

        }
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
        {
            return;
        }
        inventoryUI.CreateDraggedItem(itemIndex, inventoryItem.item, inventoryItem.item.icon, inventoryItem.quantity);
    }


    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    public void Awake()
    {
        // Load();    
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Player_pocket.status == false)
            {
                Player_pocket.show();
            }
            else
            {
                Player_pocket.hide();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            if (Foreign_Exchange.status == false)
            {
                Foreign_Exchange.show();
            } else
            {
                Foreign_Exchange.hide();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false) 
            {
                inventoryUI.show();
                WeaponBoxUI.show();
                miniInventoryUI.show();

                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
                    
                }
                initialItems = inventoryData.getInventoryItems();

                foreach(var item in WeaponBoxData.GetCurrentInventoryState())
                {
                    WeaponBoxUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
                }
                WeaponItems = WeaponBoxData.getInventoryItems();

                foreach (var item in miniInventoryData.GetCurrentInventoryState())
                {
                    miniInventoryUI.UpdateData(item.Key, item.Value.item, item.Value.item.icon, item.Value.quantity);
                }
                miniInitialItem = miniInventoryData.getInventoryItems();
            } else
            {
                MyInventoryObj.gameObject.SetActive(true);
                MyAccountObj.gameObject.SetActive(false);
                inventoryUI.hide();
                WeaponBoxUI.hide();
                miniInventoryUI.hide();
            }
        }
    }

    private void FixedUpdate()
    {
        Dictionary<string, int> allAmount_item = new Dictionary<string, int>();

        foreach(InventoryItem item in inventoryData.GetCurrentInventoryState().Values)
        {
            if (!allAmount_item.ContainsKey(item.item.item_name))
            {
                allAmount_item[item.item.item_name] = 0;
            }
            allAmount_item[item.item.item_name] += item.quantity;
        }

        foreach(string item_name in allAmount_item.Keys)
        {
            GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>().UpdateObjectiveItem(QuestObjectiveType.CollectItem, item_name, allAmount_item[item_name]);
        }
    }
    private void OnApplicationQuit()
    {
        // Save();
    }

    // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerInventory(this);
    }

    public void Load()
    {
        PlayerInventoryData data = SavePlayerSystem.LoadPlayerInventory();

        if (data != null)
        {
            initialItems = data.initialItems;
            WeaponItems = data.WeaponItems;
            miniInitialItem = data.miniInitialItem;
        }
    }
}