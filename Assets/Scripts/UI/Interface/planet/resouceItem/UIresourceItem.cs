using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using inventory.Model;
using UnityEngine.UI;

public class UIresourceItem : MonoBehaviour
{
    public static UIresourceItem Instance { get; private set; }
    [SerializeField]
    private int index;
    [SerializeField]
    private Item item;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Image borderImage;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        this.empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(int index, Item item, Sprite spritem)
    {
        Instance = this;
        this.index = index;
        this.item = item;
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = spritem;
        this.empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
