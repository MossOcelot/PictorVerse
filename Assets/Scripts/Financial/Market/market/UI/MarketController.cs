using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    public GameObject UIMarket;

    public void OpenMarket()
    {
        UIMarket.SetActive(true);
    }

    public void CloseMarket()
    {
        UIMarket.SetActive(false);
    }
}
