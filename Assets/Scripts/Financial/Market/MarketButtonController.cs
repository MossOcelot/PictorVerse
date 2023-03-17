using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketButtonController : MonoBehaviour
{
    public enum MarketType { m1, m2, m3, m4, m5 };
    public MarketType marketType;

    public void OpenMarket()
    {
        UIMarketController marketController = GameObject.FindGameObjectWithTag(marketType.ToString()).gameObject.GetComponent<UIMarketController>();
        marketController.SetUI(true);
    }
}
