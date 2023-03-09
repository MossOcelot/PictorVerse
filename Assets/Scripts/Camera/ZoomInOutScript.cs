using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOutScript : MonoBehaviour
{
    public float ZoomChange;
    public float smoothChange;
    public float MinSize, MaxSize;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
            cam.orthographicSize -= ZoomChange * Time.deltaTime * smoothChange;
        if(Input.mouseScrollDelta.y < 0)
            cam.orthographicSize += ZoomChange * Time.deltaTime * smoothChange;

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinSize , MaxSize);

    }
}
