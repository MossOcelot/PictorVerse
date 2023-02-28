using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStock
{
    public Item item;
    public int QuantityOrder = 0;
    public LimitOrderBook orderBook;

}
