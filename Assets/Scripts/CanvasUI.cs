using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject MainUI;
    //public Transform itemParents;
    //Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        MainUI.SetActive(MainUI.activeSelf);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            MainUI.SetActive(!MainUI.activeSelf);
        }
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    MainUI.SetActive(!MainUI.activeSelf);
        //}
    }
}
