using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDescriptionAlertTemplate : MonoBehaviour
{
    public TextMeshProUGUI description;

    public void SetData(AlertAnalytic alert) 
    { 
        this.description.text = alert.text;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
