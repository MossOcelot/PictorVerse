using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class UIShowAnalytic : MonoBehaviour
{
    public List<AlertAnalytic> alert_details;
    public Image img_npc_recommend;
    public GameObject Exit;
    public Sprite[] NPC_RECOMMEND_TEMPLATE;

    public GameObject UI;
    public GameObject text;
    public Transform content_trans;
    bool IsOpen;
    int index = 0;
    public void Add_Alert_Details(int level, AlertAnalytic.NPC_RECOMMEND_TYPE type, string text)
    {
        AlertAnalytic alert = new AlertAnalytic(level, type, text);
        alert_details.Add(alert);
    }

    private void Update()
    {
        int len = alert_details.Count;
        if(len > 0 && !IsOpen)
        {
            Open();
            IsOpen = true;
        }

        if(index == len - 1)
        {
            Exit.SetActive(true);
        }
        else
        {
            Exit.SetActive(false);
        }
    }

    public void Open()
    {
        UI.SetActive(true);
        string content_text = alert_details[index].text;
        Sprite img = GetSpriteNPC(alert_details[index].type);
        SetData(content_text, img);
    }

    public void Close()
    {
        alert_details.Clear();
        index = 0;
        IsOpen = false;
        UI.SetActive(false);
    }

    public void SetData(string content, Sprite npc)
    {
        int len = content_trans.childCount;
        if(len > 0)
        {
            for(int i = 0; i < len; i++)
            {
                Destroy(content_trans.GetChild(i).gameObject);
            } 
        }
        GameObject txt = Instantiate(text, content_trans);
        txt.GetComponent<TextMeshProUGUI>().text = content;
        img_npc_recommend.sprite = npc;
    }

    public void Left()
    {
        index--;
        if(index >= 0)
        {
            string content_text = alert_details[index].text;
            Sprite img = GetSpriteNPC(alert_details[index].type);
            SetData(content_text, img);
        }
        else
        {
            index++;
        }
    }

    public void Right()
    {
        index++;
        if(index < alert_details.Count)
        {
            string content_text = alert_details[index].text;
            Sprite img = GetSpriteNPC(alert_details[index].type);
            SetData(content_text, img);
        }
        else
        {
            index--;
        }
    }

    public Sprite GetSpriteNPC(AlertAnalytic.NPC_RECOMMEND_TYPE type)
    {
        if (type == AlertAnalytic.NPC_RECOMMEND_TYPE.angry)
        {
            return NPC_RECOMMEND_TEMPLATE[0];
        } else if (type == AlertAnalytic.NPC_RECOMMEND_TYPE.recommend)
        {
            return NPC_RECOMMEND_TEMPLATE[1];
        } else
        {
            return NPC_RECOMMEND_TEMPLATE[2];
        }
    }
}

[System.Serializable]
public class AlertAnalytic
{
    public enum NPC_RECOMMEND_TYPE { angry, recommend, admire}
    public int important_recommend;
    public NPC_RECOMMEND_TYPE type;
    public string text;

    public AlertAnalytic (int important_recommend, NPC_RECOMMEND_TYPE type, string text)
    {
        this.important_recommend = important_recommend;
        this.type = type;
        this.text = text;
    }
}
