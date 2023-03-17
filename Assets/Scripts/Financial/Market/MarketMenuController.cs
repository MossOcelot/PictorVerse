using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;
    bool isClick = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(isClick)
            {
                UI.SetActive(false);
                isClick = false;
            } else
            {
                UI.SetActive(true);
                isClick = true;
            }
        }
    }
}
