using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timesystem : MonoBehaviour
{
    // Next update in second
    private int nextUpdate = 1;
    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;

    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    // Update is called once per frame

    public int[] getDateTime()
    {
        return new int[] { day, month, year, hours, minutes, seconds };
    }
    void Update()
    {

        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            UpdateEverySecond();
        }

    }

    // Update is called once per second
    void UpdateEverySecond()
    {
        DateTime date = DateTime.Now;
        day = date.Day;
        month = date.Month;
        year = date.Year + 2000;
        hours = date.Hour;
        minutes = date.Minute;
        seconds = date.Second;
    }
}
