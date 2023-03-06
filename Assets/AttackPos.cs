using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPos : MonoBehaviour
{
    private bool isActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            isActive = !isActive; 
            gameObject.SetActive(isActive); 
        }
    }
}
