using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHourActivity : MonoBehaviour
{
    public Transform[] Day_content;

    public GameObject image;
    GameObject content_day;

    public List<ActivitySprite> activity_icons;
    public void SetActivity(int day, acitivty_type type)
    {
        content_day = Instantiate(image, Day_content[day - 1]);

        foreach(ActivitySprite icon in activity_icons)
        {
            if(icon.activity_type == type)
            {
                content_day.GetComponent<Image>().sprite = icon.icon;
            }
        }
    }

    public void ResetData()
    {
        foreach(Transform trans in Day_content)
        {
            int len = trans.childCount;
            for(int i = 0; i < len; i++)
            {
                Destroy(trans.GetChild(i).gameObject);
            }
        }
    }

    [System.Serializable]
    public class ActivitySprite
    {
        
        public acitivty_type activity_type;
        public Sprite icon;
    }

    public enum acitivty_type { sleep, eat, work, Mining, shopping, hospital, bank };
}
