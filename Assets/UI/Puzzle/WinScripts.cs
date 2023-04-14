using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScripts : MonoBehaviour
{
    private int pointstoWin;
    private int currentPoints;
    public GameObject MyPuzzles;

    void Start()
    {
        pointstoWin = MyPuzzles.transform.childCount;
    }
    void Update()
    {
        
        if(currentPoints >= pointstoWin)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void AddPoints()
    {
        currentPoints++;
    }
}
