using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UICheckGoal : MonoBehaviour
{
    public Sprite[] icon_status;

    public Image icon;
    public TextMeshProUGUI title;

    public void SetData(bool status, string detail, int currentAmount, int requirement)
    {
        if (status)
        {
            icon.sprite = icon_status[0];
        } else
        {
            icon.sprite = icon_status[1];
        }

        title.text = $"{detail} ({currentAmount}/{requirement})";
    }
}
