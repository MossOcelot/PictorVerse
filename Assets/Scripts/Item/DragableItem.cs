using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
    }
}
