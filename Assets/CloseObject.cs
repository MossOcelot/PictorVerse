using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CloseObject : MonoBehaviour
{
    public GameObject objectToClose;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            objectToClose.SetActive(false);
        }
    }
}


