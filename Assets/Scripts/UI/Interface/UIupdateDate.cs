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
        DateText.text = $"{time[2]}-{time[1]}-{time[0]} {time[3]}:{time[4]}:{time[5]}";
    }
}
