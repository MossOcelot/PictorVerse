using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject invOnscreen;
    public GameObject saleInterface;

    void Start()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        invOnscreen.SetActive(invOnscreen.activeSelf);
        saleInterface.SetActive(!saleInterface.activeSelf);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            invOnscreen.SetActive(!invOnscreen.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("sale");
            saleInterface.SetActive(!saleInterface.activeSelf);
        }
    }
}
