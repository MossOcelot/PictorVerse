using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIMailCard : MonoBehaviour
{
    public TextMeshProUGUI title_text;
    public TextMeshProUGUI content_text;
    public Button clickBtn;
    public void SetData(string title, string content)
    {
        title_text.text = title;
        content_text.text = content;
    }

    
}
