using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static QuestAcceptanceConditions.CareerModel;

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
            string name_quest = GetNameQuest(MainQuest);
            if (name_quest != null) SetButtonOnDialogBox(index_MainQuestBtn, name_quest, () => AcceptingMainQuest());
            
        }   
        if (HaveSecondaryQuests)
        {
            string name_quest = GetNameQuest(SecondaryQuests);
            if (name_quest != null) SetButtonOnDialogBox(index_SecondaryQuests, name_quest, () => AcceptingSecondaryQuest());
        }
        if (HaveDailyQuest)
        {
            string name_quest = GetNameQuest(DailyQuest);
            if (name_quest != null)  SetButtonOnDialogBox(index_DailyQuest, name_quest, () => AcceptingDailyQuest());
        }
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }
    public string GetNameQuest(List<QuestEvent> ListQuest)
    {
        foreach (QuestEvent questEvent in ListQuest)
        {
            Quest quest = questEvent.quest;

            if (quest.status != Quest.QuestStatus.Completed)
            {
                return quest.information.quest_name;
            }
        }
        return null;
    }
    public void AcceptingMainQuest()
    {
        foreach(QuestEvent questEvent in MainQuest)
        {
            Quest quest = questEvent.quest;

            if (quest.status != Quest.QuestStatus.Completed)
            {
                QuestAcceptanceConditions condition = CheckConditions(quest.conditions);
                if (condition != null)
                {
                    NPCSendAlert("�س�ѧ�������ö�Ѻ��áԨ�����ͧ�ҡ", condition.description);
                    break;
                }
                if (quest.status == Quest.QuestStatus.InProgress) 
                {
                    NPCSendAlert("�س�� �Ѻ��áԨ�����", "�ô�������áԨ��͹˹��������稡�͹"); 
                    break; 
                }
                
                NpcSendQuest(questEvent);
                break;
            }
         } 
    }

    public void AcceptingSecondaryQuest()
    {
        foreach (QuestEvent questEvent in SecondaryQuests)
        {
            Quest quest = questEvent.quest;

            if (quest.status != Quest.QuestStatus.Completed)
            {
                QuestAcceptanceConditions condition = CheckConditions(quest.conditions);
                if (condition != null)
                {
                    NPCSendAlert("�س�ѧ�������ö�Ѻ��áԨ�����ͧ�ҡ", condition.description);
                    break;
                }
                if (quest.status == Quest.QuestStatus.InProgress)
                {
                    NPCSendAlert("�س�� �Ѻ��áԨ�����", "�ô�������áԨ��͹˹��������稡�͹");
                    break;
                }

                NpcSendQuest(questEvent);
                break;
            }
        }
    }

    public void AcceptingDailyQuest()
    {
        foreach (QuestEvent questEvent in DailyQuest)
        {
            Quest quest = questEvent.quest;

            if (quest.status != Quest.QuestStatus.Completed)
            {
                QuestAcceptanceConditions condition = CheckConditions(quest.conditions);
                if (condition != null)
                {
                    NPCSendAlert("�س�ѧ�������ö�Ѻ��áԨ�����ͧ�ҡ", condition.description);
                    break;
                }
                if (quest.status == Quest.QuestStatus.InProgress)
                {
                    NPCSendAlert("�س�� �Ѻ��áԨ�����", "�ô�������áԨ��͹˹��������稡�͹");
                    break;
                }

                NpcSendQuest(questEvent);
                break;
            }
        }
    }

    public QuestAcceptanceConditions CheckConditions(List<QuestAcceptanceConditions> conditions)
    {
        foreach(QuestAcceptanceConditions condi in conditions)
        {
            if(condi.type == QuestAcceptanceConditions.QuestAcceptanceType.Career)
            {
                CareerPlayer career = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CareerPlayer>();

                bool status = CheckCareer(career.Career, condi);
                if (!status)
                {
                    return condi;
                }
               
            }
        }
        return null;
    }

    public bool CheckCareer(CareerSO career, QuestAcceptanceConditions condi)
    {
        if (condi.career_data.type == CareerModelType.none)
        {
            if (career == null) return true;
            return false;
        }

        if (career.career_name == condi.career_data.careerName)
        {
            return true;
        }
        return false;
    }
    public void NPCSendAlert(string greeting, string convensation)
    {
        npcController.RemoveText();
        npcController.IsEndSituation = false;
        npcController.dialogue.Clear();
        npcController.dialogue.Add(greeting);
        npcController.dialogue.Add(convensation);

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
