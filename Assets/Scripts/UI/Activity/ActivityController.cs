using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;
public class ActivityController : MonoBehaviour
{
    public UIHourActivity[] hourActivitys;
    public int firstYear;

    private void Start()
    {
        firstYear = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().startYear;
    }

    public void SetActivityInHoursDay(int[] date, UIHourActivity.acitivty_type acitivty_Type)
    {
        int hours = date[3];
        int minutes = date[4];

        int index = hours - 8;
        if(minutes == 0)
        {
            index -= 1;
        }
        if(index < 0)
        {
            index = 24 + index;
        }

        int day = (date[0] + (12 * (date[1] - 1) * ((firstYear - date[2]) + 1))) % 7;
        if(day == 0)
        {
            day = 7;
        }
        Debug.Log($"Index: {index} Day: {day}");
        //SetToDayWeek();
        hourActivitys[index].SetActivity(day, acitivty_Type);
    }
}
