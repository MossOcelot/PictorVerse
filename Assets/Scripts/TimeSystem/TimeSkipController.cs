using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSkipController : MonoBehaviour
{
    public Transform ButtonList;
    public NPCController npcController;
    public GameObject TimeLineSleep;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void Update()
    {
        if (!npcController.playerIsClose) return;
        bool IsEndSituation = npcController.IsEndSituation;

        if (!IsEndSituation)
        {
            ButtonList = GameObject.FindGameObjectWithTag("Dialog").gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform;

            return;
        }
        if (IsEndSituation && npcController.playerIsClose)
        {
            AddButtonInDialog();
        }
    }

    public void AddButtonInDialog()
    {
        SetButtonOnDialogBox(1, "Sleep", () => SkipDay());
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }
    private IEnumerator SleepRoutine()
    {
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        TimeLineSleep.SetActive(true);
        yield return new WaitForSeconds(12f);
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        TimeLineSleep.SetActive(false);
        Timesystem time = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        time.SkipDay();

        PlayerActivityController activity_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
        activity_controller.AddActivity(time.getDateTime(), UIHourActivity.acitivty_type.sleep);

        npcController.dialoguePanel.SetActive(false);
    }
    public void SkipDay()
    {
        if (TimeLineSleep.activeSelf)
        {
            return;
        }
        else
        {
            StartCoroutine(SleepRoutine());
        }
    }
}