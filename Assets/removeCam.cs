using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeCam : MonoBehaviour
{
    public float timeToremove;
    private void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(timeToremove);
        Destroy(gameObject);
    }
}
