using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LimitOrder
{
    public string OrderId;
    [SerializeField]
    public float Price;
    [SerializeField] 
    public float GainPrice = 0;
    [SerializeField]
    public int GainQuantity = 0;
    [SerializeField]
    public int Quantity;
    [SerializeField]
    public bool IsBuy;
    [SerializeField]
    public int status = 0;
    public LimitOrder(string orderId,int price, int quantity, bool isBuy)
    {
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
        IsBuy = isBuy;
    }
}
