using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UISettingBar : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingBar;
    bool isClick = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isClick)
            {
                SettingBar.SetActive(false);
                isClick = false;
            }
            else 
            { 
                SettingBar.SetActive(true);
                isClick = true;
            }
        }
    }
}
