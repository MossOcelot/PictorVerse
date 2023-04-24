using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScripts : MonoBehaviour
{
    public int pointstoWin;
    public int currentPoints;
    public GameObject MyPuzzles;
    public bool WinNow = false;
    public NPCController npccontroller;
    public GameObject Complete;
    public GameObject player;
    public Camera mainCamera;

    public GameObject NextQuestSign;

    private IEnumerator SetCompleteActiveCoroutine()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        npccontroller = FindObjectOfType<NPCController>();
        WinNow = false;
        pointstoWin = MyPuzzles.transform.childCount;
    }

    void Update()
    {
        if (currentPoints >= pointstoWin && !WinNow)
        {
            WinNow = true;
            npccontroller.isPuzzling = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                npccontroller.TimeLine.gameObject.SetActive(false);
                mainCamera.orthographicSize = 3.9f;
            }
            if (NextQuestSign != null)
            {
                NextQuestSign.SetActive(true);
            }
            npccontroller.isPuzzle = false;
            player.gameObject.GetComponent<PlayerMovement>().isLooking = false;
            Debug.Log("Winnow");
            StartCoroutine(SetCompleteActiveCoroutine());
        }
    }

    public void AddPoints()
    {
        currentPoints++;
    }
}
