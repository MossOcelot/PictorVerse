using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeCam : MonoBehaviour
{
    public float timeToremove;

    private void Start()
    {
        Invoke("RemoveObject", timeToremove);
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
