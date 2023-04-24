using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivity : MonoBehaviour
{
    public GameObject UI;
    public ActivityController activity_controller;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            Debug.Log("Slash");
            if(UI.activeSelf)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Open()
    {
        PlayerActivityController player_activity = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
        Timesystem time = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        List<ActivityInMyLife> activity_all = player_activity.activityList;

        UI.SetActive(true);

        int[] present_time = time.getDateTime();
        int day_present = (present_time[0] + (12 * (present_time[1] - 1) * ((time.startYear - present_time[2]) + 1)));
        foreach (ActivityInMyLife activity in activity_all)
        {
            int[] date = activity.date;
            int day = (date[0] + (12 * (date[1] - 1) * ((time.startYear - date[2]) + 1)));

            Debug.Log($"Day difference: {day_present - day}");
            if(day_present - day > 7)
            {
                break;
            }
            activity_controller.SetActivityInHoursDay(date, activity.activity);
        }
    }

    public void Close()
    {
        foreach(UIHourActivity hours_content in activity_controller.hourActivitys)
        {
            hours_content.ResetData();
        }
        UI.SetActive(false);
    }
}
