using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIStatusEffectDescription : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI Head_name;
    public TextMeshProUGUI Effect_description;
    public TextMeshProUGUI description;
    public TextMeshProUGUI time;
    public int counter;

    int old_time;
    public void SetData(StatusEffectPlayer data)
    {
        icon.sprite = data.data.icon;
        Head_name.text = data.data.effect_name;
        Effect_description.text = data.data.EffectDetails;
        description.text = data.data.description;
        time.text = $"ระยะเวลาที่เหลือ {data.data.Lifetime - data.current_time} วินาที";
        counter = data.data.Lifetime - data.current_time;
    }

    private void Update()
    {
        int[] times = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
        
        if(old_time != times[4])
        {
            counter--;
            time.text = $"ระยะเวลาที่เหลือ {counter} วินาที";
            old_time = times[4];
            if(counter < 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
