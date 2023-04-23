using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UISettingBar : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingBar;
    [SerializeField] bool isClick = false;
    public bool CanExit = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CanExit)
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
            } else
            {
                CanExit = true;
            }
            
        }
    }
}
