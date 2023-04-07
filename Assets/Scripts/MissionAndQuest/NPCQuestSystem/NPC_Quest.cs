using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Quest : MonoBehaviour
{
    [System.Serializable]
    public class QuestEvent
    {
        public Quest quest;
        public QuestDialogue Dialogue;
    }
    [SerializeField]
    private List<QuestEvent> MainQuest;
    public bool HaveMainQuest;
    public int index_MainQuestBtn;
    [SerializeField]
    private List<QuestEvent> SecondaryQuests;
    public bool HaveSecondaryQuests;
    public int index_SecondaryQuests;
    [SerializeField]
    private List<QuestEvent> DailyQuest;
    public bool HaveDailyQuest;
    public int index_DailyQuest;

    public Transform ButtonList;

    public NPCController npcController;

    Quest quest;

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
            if(!npcController.IsInQuest) AddButtonInDialog();
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
        foreach(QuestEvent questEvent in MainQuest)
        {
            Quest quest = questEvent.quest;

            if (quest.status != Quest.QuestStatus.Completed)
            {
                if (quest.status == Quest.QuestStatus.InProgress) 
                {
                    NPCSendAlert(); 
                    break; 
                }
                NpcSendQuest(questEvent);
                break;
            }
         } 
    }
    
    public void NPCSendAlert()
    {
        npcController.RemoveText();
        npcController.IsEndSituation = false;
        npcController.dialogue.Clear();
        npcController.dialogue.Add("คุณได้ รับภารกิจไปแล้ว");
        npcController.dialogue.Add("โปรดทำให้ภารกิจก่อนหน้าให้เสร็จก่อน");

        npcController.SetIndexConversation(0);

        npcController.IsInQuest = true;
        npcController.StartDialogue();
    }

    public void NpcSendQuest(QuestEvent questEvent)
    {

        QuestDialogue dialogue = questEvent.Dialogue;
        npcController.RemoveText();
        npcController.IsEndSituation = false;
        npcController.dialogue.Clear();
        npcController.dialogue.Add(dialogue.greeting);
        npcController.dialogue.AddRange(dialogue.conversation);

        npcController.SetIndexConversation(0);

        quest = questEvent.quest;

        npcController.IsInQuest = true;
        npcController.StartDialogue();
    }

    public void SendQuest()
    {
        if (quest == null) return;
        quest.status = Quest.QuestStatus.InProgress;
        GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>().QuestList.QuestList.Add(quest);
        quest = null;
    }



}
