using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemTemplate;
    private GameObject g;

    private Item_detail item_detail;
    [SerializeField]
    private Transform ShopScrollView;

    private List<Dictionary<string, dynamic>> item_shop_list;

    private void Start()
    {
        item_shop_list = new List<Dictionary<string, dynamic>>()
        {
            new Dictionary<string, dynamic>()
            {
                { "Item_name", "pao"},
                { "color_tag", new Color32(255,200,20,255)},
                { "price_item", 100 }
            },
            new Dictionary<string, dynamic>()
            {
                { "Item_name", "moss"},
                { "color_tag", new Color32(155,150,120,255)},
                { "price_item", 1000000 }
            }
        };

        
        foreach (Dictionary<string, dynamic> item in item_shop_list)
        {
            

            

            ItemTemplate.GetComponent<Item_detail>().set_data(item["Item_name"],
            item["color_tag"],
            item["price_item"]);
            g = Instantiate(ItemTemplate, ShopScrollView);
        }
       
    }
}
