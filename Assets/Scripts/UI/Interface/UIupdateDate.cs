using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIupdateDate : MonoBehaviour
{
    public TextMeshProUGUI DateText;
    public Timesystem timeSystem;

    private void Start()
    {
        timeSystem = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
    }
    private void FixedUpdate()
    {
        int[] time = timeSystem.getDateTime();
        DateText.text = time[2].ToString() + "-" + time[1].ToString("00") + "-" + time[0].ToString("00") + " "
            + time[3].ToString("00") + " ชั่วโมง " + time[4].ToString("00") + " นาที";
    }
}
