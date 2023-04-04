using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;

[CreateAssetMenu(fileName = "New NPC_SHOP_DATA", menuName = "NPC/NPC_Shop")]
public class NPCShopData : ScriptableObject
{
    public Financial_Details financialDetail;
    public List<InventoryItem> buy_items_list;
    public List<InventoryItem> resource;
    public List<string> product_type;
    public List<AccountsDetail> accountsDetails;
}
