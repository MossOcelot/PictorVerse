using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timesystem : MonoBehaviour
{
    public float minutesPerSecond = 1.2f;
    public int startYear = 4023;
    public int startMonth = 1;
    public int startDay = 1;
    public int startHour = 8;
    public int daysPerMonth = 12;
    public int monthPerYear = 12;

    public float _timeOfDay = 0f;
    // Next update in second
    private int nextUpdate = 1;
    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;

    [SerializeField] private int hours;
    [SerializeField] private float minutes;

    // Update is called once per frame
    public int[] getDateTime()
    {
        return new int[] { day, month, year, hours, Mathf.RoundToInt(minutes) };
    }

    private void Start()
    {
        year = startYear;
        month = startMonth;
        day = startDay;
        hours = startHour;
        _timeOfDay = (float)hours;
    }
    void Update()
    {
        _timeOfDay += Time.deltaTime * minutesPerSecond / 60f;
        if(_timeOfDay > 24f)
        {
            _timeOfDay -= 24f;
            day++;
            if (day > daysPerMonth)
            {
                day = 1;
                month++;
            }
            if (month > monthPerYear)
            {
                month = 1;
                year++;
            }
        }

        minutes = (_timeOfDay * 60) - ((float)hours * 60f);
        if (minutes < 0) minutes = 0;
        if(minutes > 59f)
        {
            minutes = 0;
            hours++;

            if(hours > 23)
            {
                Debug.Log("Full Time");
                hours = 0;
            };
            
        }

        // float timePercent = _timeOfDay / 24f;
        // float sunAngle = Mathf.Lerp(-90f, 270f, timePercent);
        // transform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

    }
}
