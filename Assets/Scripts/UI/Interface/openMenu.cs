using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panelToClose;

    bool isOpen = false;

    public void openPanel()
    {
        if (isOpen!=true)
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
        else
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
        
    }
}
