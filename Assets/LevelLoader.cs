using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public float outOfRange = 1.5f;
    public Camera mainCamera;
    public List<Transform> childTransforms;

    private void Start()
    {

        mainCamera = Camera.main;

        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        foreach (Transform childTransform in childTransforms)
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(childTransform.position);

            if (viewportPosition.x > -outOfRange && viewportPosition.x < 1 + outOfRange && viewportPosition.y > -outOfRange && viewportPosition.y < 1 + outOfRange)
            {
                childTransform.gameObject.SetActive(true);
            }
            else
            {
                childTransform.gameObject.SetActive(false);
            }
        }
    }
}