using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_Reach : MonoBehaviour
{
    public GameObject missionComplete;
    public GameObject signReach;

    [System.Serializable]
    public class ReachQuestEvent
    {
        public bool status;
        public Quest quest;
        public QuestDialogue dialogue;
        public NPC_Quest nextQuest;
    }

    public List<ReachQuestEvent> questEvents;
    public string objectName;

    private Quest presentQuest;

    public void Update()
    {
        foreach (ReachQuestEvent reach in questEvents)
        {
            if (reach.status && reach.nextQuest != null)
            {
                reach.nextQuest.gameObject.SetActive(true);
            }
        }
    }

    public QuestDialogue CheckQuestInPlayer()
    {
        MissionCanvasController missionPlayer = GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>();
        List<Quest> quests = missionPlayer.QuestList.QuestList;
        foreach (Quest quest in quests)
        {
            if (quest.status != Quest.QuestStatus.InProgress) continue;
            foreach (ReachQuestEvent reach in questEvents)
            {
                Quest reachQuest = reach.quest;
                if (reachQuest.information.quest_name == quest.information.quest_name)
                {

                    if (reach.status) return null;
                    presentQuest = quest;
                    reach.status = true;
                    signReach.SetActive(true);
                    return reach.dialogue;
                }
            }
        }
        return null;
    }

    public void FinishReach()
    {
        List<QuestObjective> goals = presentQuest.goals;

        foreach (QuestObjective objective in goals)
        {
            if (objective.type == QuestObjective.QuestObjectiveType.ReachLocation)
            {
                if (objective.name_object == objectName)
                {
                    objective.currentAmount += 1;
                    if (objective.currentAmount >= objective.targetAmount)
                    {
                        signReach.SetActive(false);
                        objective.completed = true;
                    }
                    break;
                }
            }
        }

        if (missionComplete != null)
        {
            missionComplete.SetActive(true);
        }

        presentQuest.UpdateGoals();

        foreach (ReachQuestEvent reach in questEvents)
        {
            if (reach.quest == presentQuest)
            {
                reach.nextQuest.gameObject.SetActive(true);
                break;
            }
        }
    }
}
