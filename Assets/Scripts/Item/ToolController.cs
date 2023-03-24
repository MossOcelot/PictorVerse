using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    PlayerMovement playermov;
    Rigidbody2D rb;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float pickupZone = 1.5f;
    public Animator animator;

    private void Awake()
    {
        playermov = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Debug.Log("A1");
        bool mouseOverItemCutD = false;
        Collider2D mouseOverCollider = GetMouseOverCollider();
        Debug.Log("A2");
        while (mouseOverCollider != null && (mouseOverCollider.tag == "ItemCutD" || mouseOverCollider.tag == "DT"))
        {
            Debug.Log("A3");
            mouseOverItemCutD = true;
            Tool hit;
            Debug.Log("A4");
            if (Input.GetMouseButtonDown(0) && mouseOverCollider == GetMouseOverCollider())
            {
                Debug.Log("A5");
                if (mouseOverCollider.TryGetComponent(out hit))
                {

                    hit?.Hit();
                    Debug.Log("A6");
                    break;
                }
            }
            mouseOverCollider = GetMouseOverCollider();
            Debug.Log("A7");
        }
        Debug.Log("A8");
        if (mouseOverItemCutD)
        {
            Debug.Log("B");
            animator.SetTrigger("SwordAttack");
            Debug.Log("A9");
            UseTool();
            Debug.Log("A10");
        }
        /*else
        {
            Debug.Log("C");
            animator.SetBool("isMoving", true);
            Debug.Log("A11");
            Debug.Log("Not ItemCutD");
            Debug.Log("A12");
        }*/
        Debug.Log("A13");
    }

    private Collider2D GetMouseOverCollider()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        foreach (Collider2D c in colliders)
        {
            if (c.tag == "ItemCutD" || c.tag == "DT")
            {
                return c;
            }
        }

        return null;
    }
}