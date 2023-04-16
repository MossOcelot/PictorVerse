using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerActivityController : MonoBehaviour
{
    public List<ActivityInMyLife> activityList; 

    public void AddActivity(int[] date, UIHourActivity.acitivty_type activity)
    {
        ActivityInMyLife ActivityLife = new ActivityInMyLife(date, activity);
        bool status = CheckActivity(date, activity);
        if (!status) return;
        
        activityList.Add(ActivityLife);
    }

    public bool CheckActivity(int[] date, UIHourActivity.acitivty_type activity)
    {
        foreach(ActivityInMyLife actMyLife in activityList)
        {
            int[] time = actMyLife.date;
            if (time[0] == date[0] && time[1] == date[1] && time[2] == date[2])
            {
                if (time[3] == time[3])
                {
                    if (time[4] > 0 && date[4] > 0)
                    {
                        if(actMyLife.activity == activity) return false;
                        
                    }
                    else
                    if (time[4] == 0 && date[4] == 0)
                    {
                        if (actMyLife.activity == activity) return false;
                        
                    }
                }
            }
        }
        return true;
    }
}

[System.Serializable]
public class ActivityInMyLife
{
    public int[] date;
    public UIHourActivity.acitivty_type activity;

    public ActivityInMyLife(int[] date, UIHourActivity.acitivty_type activity)
    {
        this.date = date;
        this.activity = activity;
    }
}
