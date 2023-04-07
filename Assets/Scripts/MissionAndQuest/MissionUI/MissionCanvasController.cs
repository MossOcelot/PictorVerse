using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Quest;
using static QuestObjective;

public class MissionCanvasController : MonoBehaviour
{
    public GameObject UI;


    [SerializeField]
    private Transform MainQuestContent;
    [SerializeField]
    private Transform SecondaryQuestContent;
    [SerializeField]
    private Transform DailyQuestContent;

    public QuestListPlayer QuestList;

    public GameObject QuestPanelTemplate;
    GameObject QuestCard;
    public List<GameObject> QuestPanelGroup = new List<GameObject>();

    public Transform DetailQuestContent;
    public GameObject DetailQuestTemplate;
    GameObject DetailQuest;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(UI.activeInHierarchy == false)
            {
                Open();
                FirstShowQuest();
            } else
            {
                Close();
            }
        }
    }

    public void Open()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().SetIsLooking(true);
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = true;
        UI.SetActive(true);

        UpdateQuest();
    }

    public void Close()
    {
        ClearQuestCard();
        UI.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().isLooking = false;

        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = false;
    }

    public void ClearQuestCard() 
    {
        int len = QuestPanelGroup.Count;
        if (len > 0)
        {
            Debug.Log("len: " + len);
            for (int i = 0; i < len; i++)
            {
                Destroy(QuestPanelGroup[i].gameObject);

            }
            QuestPanelGroup.Clear();
        }
    }
    private void UpdateQuest()
    {
        foreach (Quest quest in QuestList.QuestList)
        {
            string quest_type = quest.questType.ToString();

            if(quest_type == "MainQuest")
            {
                QuestCard = Instantiate(QuestPanelTemplate, MainQuestContent);
            } 
            else if (quest_type == "SecondaryQuest")
            {
                QuestCard = Instantiate(QuestPanelTemplate, SecondaryQuestContent);
            } 
            else if (quest_type == "DailyQuest")
            {
                QuestCard = Instantiate(QuestPanelTemplate, DailyQuestContent);
            }
            UIQuestPanel uiQuestCard = QuestCard.gameObject.GetComponent<UIQuestPanel>();
            uiQuestCard.SetData(quest.information.icon, quest.information.quest_name);
            uiQuestCard.gameObject.GetComponent<Button>().AddEventListener(quest, ClickQuest);
            QuestPanelGroup.Add(QuestCard);
        }
    }
    public void FirstShowQuest()
    {
        if(QuestPanelGroup.Count > 0)
        {
            QuestPanelGroup[0].gameObject.GetComponent<UIQuestPanel>().SetClick(true);
            ClickQuest(QuestList.QuestList[0]);
        }
    }
    public void FirstShowQuest(Quest quest)
    {
        foreach(GameObject go in QuestPanelGroup)
        {
            string quest_name = go.gameObject.GetComponent<UIQuestPanel>().quest_name.text;
            if(quest_name == quest.information.quest_name)
            {
                go.gameObject.GetComponent<UIQuestPanel>().SetClick(true);
                break;
            }
        }
        ClickQuest(quest);
    }
    private void ClickQuest(Quest quest)
    {
        foreach(GameObject obj in QuestPanelGroup)
        {
            UIQuestPanel uiQuestCard = obj.gameObject.GetComponent<UIQuestPanel>();
            if (uiQuestCard.GetQuestName() == quest.information.quest_name) continue;
            uiQuestCard.ResetClick();
            
        }
        int len = DetailQuestContent.childCount;
        if(len > 0 )
        {
            for(int i = 0; i < len; i++) 
            {
                Destroy(DetailQuestContent.gameObject.transform.GetChild(i).gameObject);
            }
        }
        DetailQuest = Instantiate(DetailQuestTemplate, DetailQuestContent);
        UIDetailQuest detailQuest = DetailQuest.gameObject.GetComponent<UIDetailQuest>();
        detailQuest.SetData(quest);
    }

    public void UpdateObjective(QuestObjectiveType objectiveType, string name_object, int amount)
    {
        Debug.Log("Finish");
        foreach (Quest quest in QuestList.QuestList)
        {
            if(quest.status == QuestStatus.InProgress)
            {
                foreach (QuestObjective goal in quest.goals)
                {
                    if (goal.type == objectiveType && goal.name_object == name_object)
                    {
                        goal.currentAmount += amount;
                        if (goal.currentAmount >= goal.targetAmount)
                        {
                            goal.completed = true;
                        }
                    }
                }
            }
            quest.UpdateGoals();
        }
    }

    public void UpdateObjectiveItem(QuestObjectiveType objectiveType, string name_object, int amount)
    {
        Debug.Log("Finish");
        foreach (Quest quest in QuestList.QuestList)
        {
            if (quest.status == QuestStatus.InProgress)
            {
                foreach (QuestObjective goal in quest.goals)
                {
                    if (goal.type == objectiveType && goal.name_object == name_object)
                    {
                        goal.currentAmount = amount;
                        if (goal.currentAmount >= goal.targetAmount)
                        {
                            goal.completed = true;
                        }
                    }
                }
            }
            quest.UpdateGoals();
        }
    }
}
