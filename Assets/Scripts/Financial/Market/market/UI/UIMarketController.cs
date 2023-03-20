using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMarketController : MonoBehaviour
{
    public GameObject UIMarket;

    public void SetUI(bool status)
    {
        UIMarket.SetActive(status);
    }
}
