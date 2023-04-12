using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_Reach : MonoBehaviour
{
    public GameObject MissionComplete;
    public GameObject SignReach;


    [System.Serializable]
    public class ReachQuestEvent
    {
        public bool status;
        public Quest quest;
        public QuestDialogue Dialogue;
    }

    public List<ReachQuestEvent> QuestEvents;
    public string object_name;


    
    
    private Quest Present_quest;


    
    public QuestDialogue CheckQuestInPlayer()
    {
        MissionCanvasController missionPlayer = GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>();
        List<Quest> quests = missionPlayer.QuestList.QuestList;
        foreach(Quest quest in quests)
        {
            if (quest.status != Quest.QuestStatus.InProgress) continue;
            foreach (ReachQuestEvent reach in QuestEvents)
            {
                Quest ReachQuest = reach.quest;
                if (ReachQuest.information.quest_name == quest.information.quest_name)
                {

                    if (reach.status)return null;
                    Present_quest = quest;
                    reach.status = true;
                    SignReach.SetActive(true);
                    return reach.Dialogue;
                }
            }
        }
        return null;
    }

    public void FinishReach()
    {
        List<QuestObjective> goals = Present_quest.goals;

        foreach(QuestObjective objective in goals)
        {
            if(objective.type == QuestObjective.QuestObjectiveType.ReachLocation)
            {
                if(objective.name_object == object_name)
                {
                    objective.currentAmount += 1;
                    if (objective.currentAmount >= objective.targetAmount)
                    {
                        SignReach.SetActive(false);
                        objective.completed = true;
                    }
                    break;
                }
            }
        }
        if (MissionComplete != null)
        {
            MissionComplete.SetActive(true);


        }
        Present_quest.UpdateGoals();
    }
}
