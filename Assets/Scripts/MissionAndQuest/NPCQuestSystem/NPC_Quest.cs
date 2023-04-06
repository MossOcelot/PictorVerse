using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Quest : MonoBehaviour
{
    [SerializeField]
    private List<Quest> MainQuest;
    public bool HaveMainQuest;
    public int index_MainQuestBtn;
    [SerializeField]
    private List<Quest> SecondaryQuests;
    public bool HaveSecondaryQuests;
    public int index_SecondaryQuests;
    [SerializeField]
    private List<Quest> DailyQuest;
    public bool HaveDailyQuest;
    public int index_DailyQuest;

    public Transform ButtonList;

    public NPCController npcController;
    public void Update()
    {
        if (!npcController.playerIsClose) return;
        bool IsEndSituation = npcController.IsEndSituation;

        if (!IsEndSituation)
        {
            ButtonList = GameObject.FindGameObjectWithTag("Dialog").gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform;

            return;
        }
        if (IsEndSituation && npcController.playerIsClose)
        {
            AddButtonInDialog();
        }
    }

    public void SetButton(Transform ButtonList)
    {
        this.ButtonList = ButtonList;
    }

    public void AddButtonInDialog()
    {
        if (HaveMainQuest)
        {
            SetButtonOnDialogBox(index_MainQuestBtn, "รับภารกิจหลัก", () => AcceptingMainQuest());
        }
        if (HaveSecondaryQuests)
        {
            SetButtonOnDialogBox(index_SecondaryQuests, "รับภารกิจรอง", null);
        }
        if (HaveDailyQuest)
        {
            SetButtonOnDialogBox(index_DailyQuest, "รับภารกิจประจำวัน", null);
        }
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public void AcceptingMainQuest()
    {
        foreach(Quest quest in MainQuest)
        {
            if (!quest.Completed)
            {
                GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>().QuestList.Add(quest);
                break;
            }
        }
    }
}
