using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalsContent;
    [SerializeField] private TextMeshProUGUI Currency;

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.Name;
        descriptionText.text = quest.Information.Description;
        
        foreach(Quest.QuestGoal goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalObj.transform.Find("text").GetComponent<TextMeshProUGUI>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            GameObject skipObj = goalObj.transform.Find("Skip").gameObject;

            if(goal.Completed)
            {
                countObj.SetActive(false);
                skipObj.SetActive(false);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }
            else
            {
                countObj.GetComponent<TextMeshProUGUI>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;

                skipObj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    goal.Skip();

                    countObj.SetActive(false);
                    skipObj.SetActive(false);
                    goalObj.transform.Find("Done").gameObject.SetActive(true);
                });
            }
        }

        Currency.text = quest.Reward.Currency.ToString("F");
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);

        int len = goalsContent.childCount;

        for(int i = 0; i < len; i++)
        {
            Destroy(goalsContent.GetChild(i).gameObject);
        }
    }
}
