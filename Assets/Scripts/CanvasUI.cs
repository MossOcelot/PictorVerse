using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject invOnscreen;

    void Start()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        invOnscreen.SetActive(invOnscreen.activeSelf);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            invOnscreen.SetActive(!invOnscreen.activeSelf);
        }
    }
}
