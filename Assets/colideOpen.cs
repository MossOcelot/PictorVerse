using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colideOpen : MonoBehaviour
{
    public GameObject CanvasBox;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("craftingTable") && Input.GetKeyDown(KeyCode.X))
        {
            CanvasBox.SetActive(true);
        }
        else
        {
            CanvasBox.SetActive(false);
        }
    }
}

