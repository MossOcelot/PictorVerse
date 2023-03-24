using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;
public class AllItemInMarket : MonoBehaviour
{
    public class ItemData
    {
        public int quantity;
        public float price;

        public ItemData (int quantity, float price)
        {
            this.quantity = quantity;
            this.price = price;
        }
    }
    private Dictionary<Item, ItemData> itemsInMarkets;
    private Timesystem time_system;

    [SerializeField]
    private int beforeDay;

    public Dictionary<Item, ItemData> GetitemsInMarket()
    {
        return itemsInMarkets;
    }
    // Start is called before the first frame update
    void Start()
    {
        time_system = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();  
    }

    // Update is called once per frame
    void Update()
    {
        int day = time_system.getDateTime()[0];
        if (time_system.getDateTime()[0] != beforeDay)
        {
            int len = gameObject.transform.childCount;
            // clear data
            itemsInMarkets = new Dictionary<Item, ItemData>(); 
            for (int i = 0; i < len; i++)
            {
                List<ItemInStock> stock = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<StockSystem>().stock;
                foreach(ItemInStock category in stock)
                {
                    foreach(ItemStock item in category.itemStock)
                    {
                        Item key = item.item;
                        float price = item.orderBook.marketPrice;
                        itemsInMarkets[key] = new ItemData(item.orderBook.quantityItem, price);
                    }
                }
            }
            beforeDay= time_system.getDateTime()[0];
        }
    }
}
