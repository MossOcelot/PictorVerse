using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panelToClose;

    public GameObject miniQuest_template;
    GameObject miniQuestBox;
    List<GameObject> miniQuestBox_Group = new List<GameObject>();
    public Transform miniQuest_content;
    bool isOpen = false;

    private MissionCanvasController mission_controller;
    List<Quest> questList;
    int oldQuestListCount;
    private void Start()
    {
        mission_controller = GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>();
        
    }

    public void openPanel()
    {
        if (isOpen!=true)
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
        else
        {
            panelToClose.SetActive(!isOpen);
            isOpen = !isOpen;
        }
        
    }

    public void FixedUpdate()
    {
        questList = mission_controller.QuestList.QuestList;
        int len = questList.Count;
        
        if (len != oldQuestListCount)
        {
            if(miniQuestBox_Group.Count > 0)
            {
                foreach(GameObject go in miniQuestBox_Group)
                {
                    Destroy(go.gameObject);
                }
            }
            foreach (Quest quest in questList)
            {
                miniQuestBox = Instantiate(miniQuest_template, miniQuest_content);
                if (quest.status == Quest.QuestStatus.Completed)
                {
                    miniQuestBox.gameObject.GetComponent<UIMiniQuestBox>().SetData(quest,"Completed" , quest.information.quest_name, quest.information.description);
                } else
                {
                    miniQuestBox.gameObject.GetComponent<UIMiniQuestBox>().SetData(quest,quest.questType.ToString(), quest.information.quest_name, quest.information.description);
                }
               
                miniQuestBox.gameObject.GetComponent<Button>().AddEventListener(quest, ClickOpenQuest);
                miniQuestBox_Group.Add(miniQuestBox);
            }
            oldQuestListCount = len;
        }
    }

    private void ClickOpenQuest(Quest quest)
    {
        mission_controller.Open();
        mission_controller.FirstShowQuest(quest);
    }
}
