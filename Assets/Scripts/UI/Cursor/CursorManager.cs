using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D clickTexture;

    private CursorControl Controls;
    private Vector2 cursorHospot;


    private void Awake()
    {
        Controls = new CursorControl();
        ChangeCursor(cursorTexture);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    private void Start()
    {
        Controls.Mouse.Click.started += _ => StartedClick();
        Controls.Mouse.Click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(clickTexture);
    }

    private void EndedClick()
    {
        ChangeCursor(cursorTexture);
    }

    private void ChangeCursor(Texture2D CursorType)
    {
        cursorHospot = new Vector2(cursorTexture.width / cursorTexture.width, cursorTexture.height / cursorTexture.height);
        Cursor.SetCursor(CursorType, cursorHospot,CursorMode.Auto);
    }
}
