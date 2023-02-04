using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject ShakeFX;
    public float ShakeDur;

    private void Start()
    {
        ShakeFX.SetActive(false);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StopAllCoroutines();
            StartCoroutine(Shake(ShakeDur));
        }
    }

    IEnumerator Shake(float t)
    {
        ShakeFX.SetActive(true);
        yield return new WaitForSeconds(t);
        ShakeFX.SetActive(false);
    }
}
