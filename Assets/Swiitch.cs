using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiitch : MonoBehaviour
{
    public GameObject[] background;
    public int maxIndex;
    public int numberElement;
    public GameObject closetoggle;
    bool toggleisClose;
    int index;

    void Start()
    {
        index = 0;
    }
    void Update()
    {
        if (index >= maxIndex)
            index = maxIndex;

        if (index < 0)
            index = 0;

        if (index == 0)
        {
            background[0].gameObject.SetActive(true);
        }
    }
    public void Next()
    {
        index += 1;
        
        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
            

        }
        Debug.Log(index);
    }
    public void Previous()
    {
        index -= 1;

        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }

    public void CloseToggle()
    {
        toggleisClose = true;

        if (toggleisClose)
        {
            closetoggle.gameObject.SetActive(false);
        }
    }
}