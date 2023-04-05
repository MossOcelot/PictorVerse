using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using System.Data.SqlTypes;

public class UIMiniQuestBox : MonoBehaviour
{
    public Sprite[] img_type;

    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void SetData(string type, string title, string description)
    {
        SetIcon(type);
        this.title.text = title;
        this.description.text = description;
    }

    private void SetIcon(string type)
    {
        if(img_type.Length == 0) return;
        
        if (type == "MainQuest")
        {
            icon.sprite = img_type[0];
        }
        else if (type == "SecondaryQuest")
        {
            icon.sprite = img_type[1];
        }
        else if (type == "DailyQuest")
        {
            icon.sprite = img_type[2];
        } 
    }
}
