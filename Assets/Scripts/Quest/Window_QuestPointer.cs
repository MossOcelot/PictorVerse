using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_QuestPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private Image pointerImage;
    private RectTransform pointerRectTransform;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;
    [SerializeField] private Vector2 targetPositions;


    private void Awake()
    {
        GameObject pointerObject = GameObject.FindWithTag("Pointer");
        if (pointerObject != null)
        {
            targetPosition = pointerObject.transform.position;
        }
        else
        {
            Debug.LogError("No GameObject with the tag 'Pointer' found!");
        }

        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("Pointer").GetComponent<Image>();
    }
    private void Update()
    {
        GameObject pointerObject = GameObject.FindWithTag("Pointer");
        if (pointerObject != null)
        {
            targetPosition = pointerObject.transform.position;
        }
        else
        {
            Debug.Log("No GameObject with the tag 'Pointer' found!");
        }

        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;

        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 150f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        if (isOffScreen)
        {
            RotatePointerTowardsTargerPosition();
            pointerImage.sprite = arrowSprite;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize; 
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize; 

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            pointerImage.sprite = crossSprite;

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);

            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
            pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }


    private void RotatePointerTowardsTargerPosition()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

}

