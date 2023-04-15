using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScripts : MonoBehaviour
{
    public int pointstoWin;
    public int currentPoints;
    public GameObject MyPuzzles;
    public bool WinNow = false;

    void Start()
    {
        WinNow = false;
        pointstoWin = MyPuzzles.transform.childCount;
    }
    void Update()
    {
        
        if(currentPoints >= pointstoWin)
        {
            WinNow = true;
            Debug.Log("Winnow");
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void AddPoints()
    {
        currentPoints++;
    }
}
