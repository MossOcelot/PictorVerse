using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAcitivity : MonoBehaviour
{
    [SerializeField]
    private NPC_Shop npc_shop;
    List<InventoryItem> stock_items;
    public int stock_quantity;
    // Start is called before the first frame update
    void Start()
    {
        stock_items = npc_shop.getBuy_items_list();
    }

    // Update is called once per frame
    void Update()
    {
        checkStock(stock_items);
    }

    void checkStock(List<InventoryItem> mystock)
    {
        int len = mystock.Count;
        for (int i = 0; i < len; i++)
        {
            InventoryItem item = mystock[i];
            if (item.quantity < stock_quantity)
            {
                // ถ้าของขาด
                Debug.Log(item.item.item_name);
            }
        }
    }
}
