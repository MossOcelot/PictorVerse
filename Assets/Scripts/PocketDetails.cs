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

    public Dictionary<string, float> getPocket()
    {
        return new Dictionary<string, float> {
            {"section1", section1_cashs},
            {"section2", section2_cashs},
            {"section3", section3_cashs},
            {"section4", section4_cashs},
            {"section5", section5_cashs},
            {"gold", gold }
        };
    }

    public List<float> getPocketList()
    {
        return new List<float> { section1_cashs, section2_cashs, section3_cashs, section4_cashs, section5_cashs, gold };
    }

    public void SetPocketList(int command, float values)
    {
        if (command == 0)
        {
            this.section1_cashs = values;
        }
        else if (command == 1)
        {
            this.section2_cashs = values;
        }
        else if (command == 2)
        {
            this.section3_cashs = values;
        }
        else if (command == 3)
        {
            this.section4_cashs = values;
        }
        else if (command == 4)
        {
            this.section5_cashs = values;
        }
        else if (command == 5)
        {
            this.gold = values;
        }
    }
    public void setPocket(string command, float values)
    {
        if (command == "section1")
        {
            this.section1_cashs = values;
        } else if (command == "section2")
        {
            this.section2_cashs = values;
        } else if (command == "section3")
        {
            this.section3_cashs = values;
        } else if (command == "section4")
        {
            this.section4_cashs = values;
        }
        else if (command == "section5")
        {
            this.section5_cashs = values;
        }
        else if (command == "gold")
        {
            this.gold = values;
        }
    }
}
