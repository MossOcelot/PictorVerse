using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIplanetItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image planetImage;

    [SerializeField]
    private Image borderImage;

    public event Action<UIplanetItem> OnItemClicked;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void SetData(Sprite sprite)
    {
        this.planetImage.gameObject.SetActive(true);
        this.planetImage.sprite = sprite;
        this.empty = false;
    }

    public void ResetData()
    {
        this.planetImage.gameObject.SetActive(false);
        this.empty = true;
    }

    public void Deselect()
    {
        this.borderImage.enabled = false;
    }


    public void Select()
    {
        borderImage.enabled = true;
    }
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
