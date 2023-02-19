using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePanelMarket : MonoBehaviour
{
    [SerializeField]
    private StockSystem stocks;
    public Text balanceText;
    public Text slotText;
    private void FixedUpdate()
    {
        balanceText.text = "$ " + stocks.getBalance();

    }
}
