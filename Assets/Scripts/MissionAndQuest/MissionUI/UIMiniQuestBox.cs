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
    public Sprite[] icon_type;
    public Image icon;
    public Image Panel_BG;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void SetData(Quest quest, string type, string title, string description)
    {
        SetBG(type);
        SetIcon(quest);
        this.title.text = title;
        this.description.text = description;
    }

    private void SetIcon(Quest quest)
    {
        if (quest.questType == Quest.QuestType.MainQuest)
        {
            icon.sprite = icon_type[0];
        }
        else if (quest.questType == Quest.QuestType.SecondaryQuest)
        {
            icon.sprite = icon_type[1];
        }
        else
        {
            icon.sprite = icon_type[2];
        }
    }

    private void SetBG(string type)
    {
        if(img_type.Length == 0) return;

        if (type == "MainQuest")
        {
            Panel_BG.sprite = img_type[0];
        }
        else if (type == "SecondaryQuest")
        {
            Panel_BG.sprite = img_type[1];
        }
        else if (type == "DailyQuest")
        {
            Panel_BG.sprite = img_type[2];
        }
        else
        {
            Panel_BG.sprite = img_type[3];
        }
    }
}
