using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDescriptionAlertTemplate : MonoBehaviour
{
    public TextMeshProUGUI description;

    public void SetData(AlertDescription alert) 
    { 
        this.description.text = alert.description;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
