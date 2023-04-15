using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    public Image[] imagePieChart;
    public float[] values;

    // Update is called once per frame
    void Update()
    {
        SetValues(values);
    }

    public void AddValues(float[] newValues)
    {
        this.values = newValues;
    }



    public void SetValues(float[] valuesToSet)
    {
        float totalValues = 0;
        for(int i = 0; i < imagePieChart.Length; i++)
        {
            totalValues += FindPercentage(valuesToSet, i);
            imagePieChart[i].fillAmount = totalValues;
        }
    }

    public float FindPercentage(float[] valueToSet, int index)
    {
        float totalAmount = 0;
        for(int i = 0; i < valueToSet.Length; i++)
        {
            totalAmount += valueToSet[i];
        }

        return valueToSet[index] / totalAmount;
    }
}
