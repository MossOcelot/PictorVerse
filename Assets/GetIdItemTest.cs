using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIdItemTest : MonoBehaviour
{
    public Item[] items;
    void Start()
    {
        foreach(Item item in items)
        {
            Debug.Log(item.GetInstanceID());
        }
    }

}
