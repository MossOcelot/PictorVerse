using inventory.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Quest", menuName = "Mission/Quest")] 
public class Quest : ScriptableObject
{
    public enum QuestType { MainQuest, SecondaryQuest, DailyQuest };

    public QuestType questType;
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public float Currency;
        public PlayerStatus.StaticValue status;
        public InventoryItem[] RewardItems;
        public bool HaveItems;
        public bool HaveStatus;
    }

    [Header("Reward")] public Stat Reward = new Stat { Currency = 1000 };

    public bool Completed { get; protected set; }
    public QuestCompletedEvent QuestCompleted;
    public List<Quest> nextQuest;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescription() 
        { 
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if(CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            // charget the player some game currency   
            Complete();
        }
    }

    public List<QuestGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();

        foreach(QuestGoal goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            // give rewards
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }

}


public class QuestCompletedEvent : UnityEvent<Quest>
{

}