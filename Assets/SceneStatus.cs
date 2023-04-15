using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStatus : MonoBehaviour
{
    public enum section { section1, section2, section3, section4, section5 }
    public string scenename;
    public string subscenename;
    public section sceneInsection;

    public section CheckSectionString(string scenename)
    {
        if(scenename == "section1")
        {
            return section.section1;
        } 
        else if(scenename == "section2")
        {
            return section.section2;
        }
        else if (scenename == "section3")
        {
            return section.section3;
        }
        else if (scenename == "section4")
        {
            return section.section4;
        }
        else if (scenename == "section5")
        {
            return section.section5;
        }
        return section.section1;
    }
}

