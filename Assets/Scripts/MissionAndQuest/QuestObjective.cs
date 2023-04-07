using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GoalQuest", menuName = "Mission/GoalQuest")]
public class QuestObjective : ScriptableObject
{
    public string name;
    public string description;
    public QuestObjectiveType type;
    public string name_object;
    public int targetAmount;
    public int currentAmount;
    public bool completed;
    public enum QuestObjectiveType
    {
        CollectItem,
        KillEnemy,
        ReachLocation
    }
}