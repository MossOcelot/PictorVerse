using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
        gameObject.SetActive(false);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
            );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val) 
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
