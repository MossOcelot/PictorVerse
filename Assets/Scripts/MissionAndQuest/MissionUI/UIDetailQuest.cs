using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDetailQuest : MonoBehaviour
{
    public TextMeshProUGUI head_name;
    public TextMeshProUGUI location_quest;
    public Transform goals_content;
    public GameObject goal_template;

    GameObject goalPanel;
    List<GameObject> goalPanel_Group = new List<GameObject>();
    public TextMeshProUGUI description_quest;
    public Transform reward_content;
    public GameObject reward_template;

    GameObject rewardPanel;

    public void SetData(Quest quest)
    {
        gameObject.SetActive(true);
        this.head_name.text = quest.Information.Name;
        this.location_quest.text = quest.Information.location;

        SetGoals(quest);
        SetReward(quest);
    }

    private void SetGoals(Quest quest)
    {
        int len = goalPanel_Group.Count;
        if(len > 0)
        {
            foreach(GameObject obj in goalPanel_Group)
            {
                Destroy(obj.gameObject);
            }
            goalPanel_Group.Clear();
        }
        foreach(Quest.QuestGoal goal in quest.Goals)
        {
            goalPanel = Instantiate(goal_template, goals_content);
            goalPanel.gameObject.GetComponent<UICheckGoal>().SetData(goal.Completed, goal.GetDescription(), goal.CurrentAmount, goal.RequiredAmount);
            goalPanel_Group.Add(goalPanel);
        }
    }

    public void SetReward(Quest quest)
    {
         
    }

}
