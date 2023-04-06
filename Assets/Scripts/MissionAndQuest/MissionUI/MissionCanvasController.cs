using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCanvasController : MonoBehaviour
{
    public GameObject UI;


    [SerializeField]
    private Transform MainQuestContent;
    [SerializeField]
    private Transform SecondaryQuestContent;
    [SerializeField]
    private Transform DailyQuestContent;

    public List<Quest> QuestList;

    public GameObject QuestPanelTemplate;
    GameObject QuestCard;
    public List<GameObject> QuestPanelGroup;

    public UIDetailQuest detailQuest;
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
                UI.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().isLooking = false;

                GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = false;
            }
        } 
    }

    public void Open()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().isLooking = true;
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = true;
        UI.SetActive(true);

        UpdateQuest();
    }
    private void UpdateQuest()
    {
        foreach (Quest quest in QuestList)
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
            uiQuestCard.SetData(quest.Information.Icon, quest.Information.Name);
            uiQuestCard.gameObject.GetComponent<Button>().AddEventListener(quest, ClickQuest);
            QuestPanelGroup.Add(QuestCard);
        }
    }
    public void FirstShowQuest()
    {
        if(QuestPanelGroup.Count > 0)
        {
            QuestPanelGroup[0].gameObject.GetComponent<UIQuestPanel>().SetClick(true);
            ClickQuest(QuestList[0]);
        }
    }
    public void FirstShowQuest(Quest quest)
    {
        foreach(GameObject go in QuestPanelGroup)
        {
            string quest_name = go.gameObject.GetComponent<UIQuestPanel>().quest_name.text;
            if(quest_name == quest.Information.Name)
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
            if (uiQuestCard.GetQuestName() == quest.Information.Name) continue;
            uiQuestCard.ResetClick();
            
        }
        detailQuest.SetData(quest);
    }
}
