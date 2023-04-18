using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScripts : MonoBehaviour
{
    public int pointstoWin;
    public int currentPoints;
    public GameObject MyPuzzles;
    public bool WinNow = false;
    public GameObject Complete;

    private IEnumerator SetCompleteActiveCoroutine()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Start()
    {
        WinNow = false;
        pointstoWin = MyPuzzles.transform.childCount;
    }

    void Update()
    {
        if (currentPoints >= pointstoWin && !WinNow)
        {
            WinNow = true;
            Debug.Log("Winnow");
            StartCoroutine(SetCompleteActiveCoroutine());
        }
    }

    public void AddPoints()
    {
        currentPoints++;
    }
}
