using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    public static UIInventoryItem Instance { get; private set; }
    [SerializeField]
    private int index;
    [SerializeField]
    private Item item;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantityTxt;
    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked,
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnEnterMouseBtn,
        OnRightMouseBtnClick;

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

    public void SetData(int index,Item item, Sprite spritem ,int quantity)
    {
        Instance = this;
        this.index = index;
        this.item = item;
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = spritem;
        this.quantityTxt.text = quantity.ToString() + "";
        this.empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if(empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData basedata)
    {
        PointerEventData data = (PointerEventData)basedata;
        if (data.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnPointerEnter()
    {
        OnEnterMouseBtn?.Invoke(this);
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetIndex()
    {
        Debug.Log(index);
        return index;
    }
}
