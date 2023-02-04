using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PocketDetails
{
    [SerializeField]
    private float section1_cashs;
    [SerializeField]
    private float section2_cashs;
    [SerializeField]
    private float section3_cashs;
    [SerializeField]
    private float section4_cashs;
    [SerializeField]
    private float section5_cashs;
    [SerializeField]
    private float gold;

    public float[] getPocket()
    {
        return new float[] { section1_cashs, section2_cashs, section3_cashs, section4_cashs, section5_cashs };
    }

    public void setPocket(string command, float values)
    {
        if (command == "s1")
        {
            this.section1_cashs = values;
        } else if (command == "s2")
        {
            this.section2_cashs = values;
        } else if (command == "s3")
        {
            this.section3_cashs = values;
        } else if (command == "s4")
        {
            this.section4_cashs = values;
        }
        else if (command == "s")
        {
            this.section5_cashs = values;
        }
        else if (command == "gold")
        {
            this.gold = values;
        }
    }
}
