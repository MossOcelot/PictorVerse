using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIQuestPanel : MonoBehaviour
{
    public Image background;
    public Image icon_quest;
    public TextMeshProUGUI quest_name;
    public bool IsClick;


    [SerializeField] private Sprite[] backgroundPanels;
    public void SetData(Sprite icon, string name)
    {
        background.sprite = backgroundPanels[0];

        icon_quest.sprite = icon;
        quest_name.text = name;
    }

    public void SetClick(bool status)
    {
        IsClick = status;
        background.sprite = backgroundPanels[1];
    }

    public void ResetClick()
    {
        background.sprite = backgroundPanels[0];
    }

    public string GetQuestName()
    {
        return quest_name.text;
    }
}
