using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interfaceCanvasController : MonoBehaviour
{
    public bool isLooking = false;
    public GameObject canvas;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLooking)
        {
            canvas.SetActive(false);
        } else
        {
            canvas.SetActive(true);
        }
    }
}
