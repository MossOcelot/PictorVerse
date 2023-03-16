using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemInMarket : MonoBehaviour
{
    private Dictionary<int, float> itemsInMarkets;
    private Timesystem time_system;

    [SerializeField]
    private int beforeDay;

    public Dictionary<int, float> GetitemsInMarket()
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
        Debug.Log("Day: " + day);
        if (time_system.getDateTime()[0] != beforeDay)
        {
            int len = gameObject.transform.childCount;
            // clear data
            itemsInMarkets = new Dictionary<int, float>(); 
            for (int i = 0; i < len; i++)
            {
                List<ItemInStock> stock = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<StockSystem>().stock;
                foreach(ItemInStock category in stock)
                {
                    foreach(ItemStock item in category.itemStock)
                    {
                        int key = item.item.item_id;
                        float price = item.orderBook.marketPrice;
                        Debug.Log("item=> " + key + " price: " + price);
                        itemsInMarkets[key] = price;
                    }
                }
            }
            beforeDay= time_system.getDateTime()[0];
        }
        Debug.Log("Size : " + itemsInMarkets.Count);
    }
}
