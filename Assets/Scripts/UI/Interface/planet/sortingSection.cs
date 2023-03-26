using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingSection : MonoBehaviour
{
    [SerializeField]
    private planetSwitch section1;

    [SerializeField]
    private planetSwitch section2;

    [SerializeField]
    private planetSwitch section3 = null;

    [SerializeField]
    private planetSwitch section4 = null;

    [SerializeField]
    private planetSwitch section5 = null;

    public planetBoxSO planetData = null;

    public int index = 0;

    bool switch1_ON = false;
    bool switch2_ON = false;
    bool switch3_ON = false;
    bool switch4_ON = false;
    bool switch5_ON = false;

    private void resetSwicth()
    {
        switch1_ON = false;
        switch2_ON = false;
        switch3_ON = false;
        switch4_ON = false;
        switch5_ON = false;
    }

    public void sortSection()
    {
        resetSwicth();
        switch1_ON = section1.passBool();
        switch2_ON = section2.passBool();
        // Debug.Log("section1" + section1.passBool());
        //switch3_ON = section3.passBool();   
        //switch4_ON = section4.passBool();
        //switch5_ON = section5.passBool();

        if (switch1_ON==true)
        {
            planetData = section1.planetData;

        }
        else if (switch2_ON==true) 
        {
            planetData = section2.planetData;
        }
        //else if (switch3_ON) { planetData = section3.planetData;}
        //else if (switch4_ON) { planetData = section4.planetData;}
        //else { planetData = section5.planetData; }

        //Debug.Log("resourceData" + resourceData);

    }

    /*public void selectingResource()
    {
        if (switch1_ON == true)
        {
            section1.selectSection(index);
            resourceData = section1.resourceData;
        }
        else if (switch2_ON == true)
        {
            section2.selectSection(index);
            resourceData = section2.resourceData;
        }
    }*/
}
