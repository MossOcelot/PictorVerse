using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;

public class UIResource : MonoBehaviour
{
    AllItemInMarket allItemInMarket;
    public GameObject ResourceCardTemplate;
    public Transform ResourceTable;

    int updateTime = 10;

    public int time;
    int nextUpdate;

    GameObject ResourceCard;
    void Awake()
    {
        allItemInMarket = GameObject.FindGameObjectWithTag("AllMarket").gameObject.GetComponent<AllItemInMarket>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            time += 1; 
            if (time >= updateTime)
            {
                int len = ResourceTable.childCount;
                if (len > 0)
                {
                    for(int i = 0; i < len; i++)
                    {
                        Destroy(ResourceTable.GetChild(i).gameObject);
                    }
                }
                foreach (Item item in allItemInMarket.GetitemsInMarket().Keys)
                {
                    ResourceCard = Instantiate(ResourceCardTemplate, ResourceTable);
                    ResourceCard.gameObject.GetComponent<UIResourceCard>().SetData(item.icon, item.item_name, allItemInMarket.GetitemsInMarket()[item].quantity, allItemInMarket.GetitemsInMarket()[item].price);
                }
                time = 0;
            }
        }
        
    }
}
