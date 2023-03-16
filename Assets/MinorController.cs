using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorControl : MonoBehaviour
{
    PlayerMovement playermov;
    Rigidbody2D rb;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float pickupZone = 1.5f;

    private void Awake()
    {
        playermov = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            print("canuse");
            UseTool();
        }
        else
        {
            print("Not Use Tool weapon");
        }

    }

    private void UseTool()
    {
        Vector2 pos = rb.position + playermov.lastPos * offsetDistance;
        Collider2D[] coliders = Physics2D.OverlapCircleAll(pos, pickupZone);

        foreach (Collider2D c in coliders)
        {
            if (c.tag == "ItemCutD" && Input.GetMouseButton(0))
            {
                Tool hit = c.GetComponent<Tool>();
                if (hit != null)
                {
                    hit.Hit();
                    break;
                }
            }

        }
    }


}