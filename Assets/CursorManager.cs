using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHospot;


    void Start()
    {
        cursorHospot = new Vector2(cursorTexture.width/cursorTexture.width, cursorTexture.height/cursorTexture.height);
        Cursor.SetCursor(cursorTexture, cursorHospot, CursorMode.Auto);
    }
}
