using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlertCircle : MonoBehaviour
{
    public Sprite[] icons;

    public Image logo;

    public void Awake()
    {
        ResetData();
    }

    public void SetData(AlertDescription alertDescription)
    {
        int alert_type_index = (int)alertDescription.type;
       
       // logo.sprite = icons[alert_type_index];
    }

    public void ResetData()
    {
        logo = null;
    }
}
