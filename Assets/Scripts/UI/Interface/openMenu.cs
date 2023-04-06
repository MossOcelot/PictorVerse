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
        questList = mission_controller.QuestList;
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
            foreach(Quest quest in questList)
            {
                miniQuestBox = Instantiate(miniQuest_template, miniQuest_content);
                miniQuestBox.gameObject.GetComponent<UIMiniQuestBox>().SetData(quest.questType.ToString(), quest.Information.Name, quest.Information.Description);
                miniQuestBox.gameObject.GetComponent<Button>().AddEventListener(quest, ClickOpenQuest);
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
