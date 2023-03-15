using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpdatePanelMarket : MonoBehaviour
{
    [SerializeField]
    private StockSystem stocks;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI slotText;
    private void FixedUpdate()
    {
        balanceText.text = "$ " + stocks.getBalance();
        int fill_slot = searchCheckSlot();
        slotText.text = fill_slot.ToString() + "/40 Slots";
    }

    private int searchCheckSlot()
    {
        Dictionary<int, InventoryItem> stock_items = stocks.player.playerInventorySO.GetCurrentInventoryState();

        return stock_items.Count;
    }
}
