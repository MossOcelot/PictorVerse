using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInventoryUI : MonoBehaviour
{
    public GameObject ExtrainventoryUI;
    public Transform ExtraitemParents;
    ExtraInventory Extrainventory;

    // Start is called before the first frame update
    void Start()
    {
        Extrainventory = ExtraInventory.instance;
        Extrainventory.onItemChangedCallback += UpdateUI;
    }

    private void Update()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        ExtraInventorySlot[] slots = GetComponentsInChildren<ExtraInventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Extrainventory.items.Count)
            {
                slots[i].AddItem(Extrainventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

}
