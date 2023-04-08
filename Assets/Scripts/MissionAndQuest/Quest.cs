using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Mission/Quest")]
public class Quest : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public Sprite icon;
        public string quest_name;
        public string description;
        public string location;
    }

    public Info information;
    public QuestType questType;
    public QuestStatus status;
    public List<QuestAcceptanceConditions> conditions;
    public List<QuestObjective> goals;

    [System.Serializable]
    public class Stat
    {
        [System.Serializable]
        public class RewardCurrency
        {
            public float amount;
            public SceneStatus.section currency;
        }

        public List<RewardCurrency> currency;
        public PlayerStatus.StaticValue staticReward;
        public int career_score;
        public List<InventoryItem> itemReward;
        public CareerSO career;
    }

    public Stat Rewards;
    public bool HaveStatic;
    public bool HaveItemReward;
    public bool HaveCareer;

    public bool CheckCareer(CareerPlayer player)
    {
        if(player.Career == null)
        {
            return true;
        }
        return false;
    }

    public void UpdateGoals()
    {
        int n = 0;
        foreach(QuestObjective goal in goals)
        {
            if(goal.completed)
            {
                n++;
            }
        }
        int len = goals.Count;
        if(n == len)
        {
            status = QuestStatus.Completed;
        }
    }

    public enum QuestStatus
    {
        None,
        InProgress,
        Completed,
        Failed
    }

    public enum QuestType
    {
        MainQuest,
        SecondaryQuest,
        DailyQuest
    }
}
