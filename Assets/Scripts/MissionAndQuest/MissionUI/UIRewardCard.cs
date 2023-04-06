using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIRewardCard : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;

    public void SetData(Sprite icon, string name)
    {
        this.icon.sprite = icon; 
        this.title.text = name;
    }
}
