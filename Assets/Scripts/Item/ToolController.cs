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
        bool mouseOverItemCutD = false;
        Collider2D mouseOverCollider = GetMouseOverCollider();

        while (mouseOverCollider != null && mouseOverCollider.tag == "ItemCutD")
        {
            mouseOverItemCutD = true;
            Tool hit;
            if (Input.GetMouseButtonDown(0) && mouseOverCollider == GetMouseOverCollider())
            {

                if (mouseOverCollider.TryGetComponent(out hit))
                {
                    hit?.Hit();
                    break;
                }
            }
            mouseOverCollider = GetMouseOverCollider();
        }

        if (mouseOverItemCutD)
        {
            animator.SetTrigger("SwordAttack");
            UseTool();
        }
        else
        {
            animator.SetBool("isMoving", true);
            Debug.Log("Not ItemCutD");
        }
    }

    private Collider2D GetMouseOverCollider()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        foreach (Collider2D c in colliders)
        {
            if (c.tag == "ItemCutD")
            {
                return c;
            }
        }

        return null;
    }
}