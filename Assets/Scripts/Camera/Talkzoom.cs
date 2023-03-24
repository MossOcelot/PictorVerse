using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Talkzoom : MonoBehaviour
{
    public Camera cameraToZoom;
    public float zoomFactor = 1.2f;
    public float smoothTime = 0.5f;

    private float targetSize;
    private float currentVelocity;

    void Start()
    {
        cameraToZoom = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Camera>();
        targetSize = cameraToZoom.orthographicSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetSize /= zoomFactor;
            StartCoroutine(SmoothZoom());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetSize *= zoomFactor;
            StartCoroutine(SmoothZoom());
        }
    }

    IEnumerator SmoothZoom()
    {
        while (Mathf.Abs(cameraToZoom.orthographicSize - targetSize) > 0.01f)
        {
            cameraToZoom.orthographicSize = Mathf.SmoothDamp(cameraToZoom.orthographicSize, targetSize, ref currentVelocity, smoothTime);
            yield return null;
        }
    }
}
