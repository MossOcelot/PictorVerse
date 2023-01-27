using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_detail : MonoBehaviour
{
//  [NonSerialized]
    public string Item_name;
 // [NonSerialized]
    public Color color_tag;
 // [NonSerialized]
    public Sprite img;
 // [NonSerialized]
    public int price_item;

    
    // Start is called before the first frame update
    public void set_data(string Item_name, Color color_tag, int price_item)
    {
        this.Item_name = Item_name;
        this.color_tag = color_tag;
        this.price_item = price_item;


        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = color_tag;
        // gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = img;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = Item_name;
        gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = price_item.ToString() + " $";
    }

    // Update is called once per frame
    
}
