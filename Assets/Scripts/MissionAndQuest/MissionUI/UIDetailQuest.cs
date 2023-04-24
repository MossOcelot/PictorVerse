using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using inventory.Model;
using System.Reflection;
using System;

public class UIDetailQuest : MonoBehaviour
{
    public Sprite[] img_type;
    public Sprite[] currency_reward_icons;
    public Sprite career_reward_icon;
    public Image image;
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
    List<GameObject> rewardPanel_Group = new List<GameObject>();

    public Button GetRewardbutton;
    public void SetData(Quest quest)
    {
        gameObject.SetActive(true);
        if (quest.questType == Quest.QuestType.MainQuest)
        {
            image.sprite = img_type[0];
        }
        else if (quest.questType == Quest.QuestType.SecondaryQuest)
        {
            image.sprite = img_type[1];
        } else
        {
            image.sprite = img_type[2];
        }
        this.head_name.text = quest.information.quest_name;
        this.location_quest.text = quest.information.location;

        SetGoals(quest);
        SetReward(quest);

        SetButton(quest);
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
        foreach(QuestObjective goal in quest.goals)
        {
            goalPanel = Instantiate(goal_template, goals_content);
            goalPanel.gameObject.GetComponent<UICheckGoal>().SetData(goal.completed, goal.description, goal.currentAmount, goal.targetAmount);
            goalPanel_Group.Add(goalPanel);
        }
    }

    public void SetReward(Quest quest)
    {
        int len = rewardPanel_Group.Count;
        if (len > 0)
        {
            foreach (GameObject obj in rewardPanel_Group)
            {
                Destroy(obj.gameObject);
            }
            rewardPanel_Group.Clear();
        }
        Quest.Stat reward = quest.Rewards;
        foreach(Quest.Stat.RewardCurrency currency in reward.currency)
        {
            rewardPanel = Instantiate(reward_template, reward_content);
            string title = $"{currency.amount}";

            rewardPanel.gameObject.GetComponent<UIRewardCard>().SetData(currency_reward_icons[(int)currency.currency], title);
            rewardPanel_Group.Add(rewardPanel);
        }
        if(reward.career_score > 0)
        {
            rewardPanel = Instantiate(reward_template, reward_content);
            string title = $"{reward.career_score} แต้ม";
            rewardPanel.gameObject.GetComponent<UIRewardCard>().SetData(career_reward_icon, title);
            rewardPanel_Group.Add(rewardPanel);
        }

        foreach (InventoryItem item in reward.itemReward)
        {
            rewardPanel = Instantiate(reward_template, reward_content);
            string title = $"{item.item.item_name} {item.quantity} อัน";
            rewardPanel.gameObject.GetComponent<UIRewardCard>().SetData(item.item.icon, title);
            rewardPanel_Group.Add(rewardPanel);
        }
    }

    public void SetButton(Quest quest)
    {
        if(quest.status == Quest.QuestStatus.Completed)
        {
            GetRewardbutton.interactable = true;
            GetRewardbutton.onClick.RemoveAllListeners();
            GetRewardbutton.AddEventListener(quest, GetReward);
        } else
        {
            
            GetRewardbutton.interactable = false;
        }
    }

    private void GetReward(Quest quest)
    {
        MissionCanvasController missionController = GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>();
        Timesystem time_system = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
       
        if (quest.HaveItemReward)
        {
            InventoryController inventoryPlayer = player.GetComponent<InventoryController>();   
            foreach(InventoryItem item in quest.Rewards.itemReward)
            {
                int amount = inventoryPlayer.inventoryData.AddItem(item);
                if(amount>0)
                {
                    int miniAmount = inventoryPlayer.miniInventoryData.AddItem(item.item, amount);
                    if(miniAmount>0)
                    {
                        Debug.Log("Send to MailBox");
                    }
                }
            }
        }
        PlayerStatus player_status = player.GetComponent<PlayerStatus>();
        if (quest.HaveStatic)
        {
            
            PlayerStatus.StaticValue staticValue = quest.Rewards.staticReward;
            PlayerStatus.StaticValue oldStatic = player_status.getMyStatic();

            player_status.setMyStatic(0, oldStatic.static_useEnergy + staticValue.static_useEnergy);
            player_status.setMyStatic(1, oldStatic.static_spendVAT + staticValue.static_spendVAT);
            player_status.setMyStatic(2, oldStatic.static_spendBuy + staticValue.static_spendBuy);
            player_status.setMyStatic(3, oldStatic.static_spendSell + staticValue.static_spendSell);
            player_status.setMyStatic(4, oldStatic.static_distanceWalk + staticValue.static_distanceWalk);
            player_status.setMyStatic(5, oldStatic.static_stability + staticValue.static_stability);
            player_status.setMyStatic(6, oldStatic.static_happy + staticValue.static_happy);
            player_status.setMyStatic(7, oldStatic.static_credibility + staticValue.static_credibility);
            player_status.setMyStatic(8, oldStatic.static_healthy + staticValue.static_healthy);
            player_status.setMyStatic(9, oldStatic.static_risk + staticValue.static_risk);

        }

        CareerPlayer career = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CareerPlayer>();
        if (quest.HaveCareer)
        {
            career.Career = quest.Rewards.career;
            if (career.Career == null)
            {
                career.RemoveCareer();
            }
        }

        if(quest.questType == Quest.QuestType.DailyQuest)
        {
            CareerPlayer careerPlayer = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CareerPlayer>();
            careerPlayer.finishDailyQuestInADay += 1;
        }

        foreach(Quest.Stat.RewardCurrency currency in quest.Rewards.currency)
        {
            float newValue = currency.amount + player_status.player_accounts.getPocket()[currency.currency.ToString()];
            player_status.player_accounts.setPocket(currency.currency.ToString(), newValue);

            int[] Now_time = time_system.getDateTime();
            SceneStatus.section section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
            AccountsDetail account_Player = new AccountsDetail() { date = Now_time, accounts_name = "เงินเดือน", account_type = quest.Rewards.incomeType.ToString(), income = currency.amount, expense = 0, currencyIncome_Type = section, currencyExpense_Type = section };
            player_status.addAccountsDetails(account_Player);

        }

        int size = missionController.QuestList.QuestList.Count;
        for(int i = 0; i < size; i++)
        {
            if (missionController.QuestList.QuestList[i].information.quest_name == quest.information.quest_name)
            {
                missionController.QuestList.QuestList.RemoveAt(i);
                break;
            }
        }
        missionController.Close();
        missionController.Open();
        gameObject.SetActive(false);
        missionController.FirstShowQuest();
        
    }
}
