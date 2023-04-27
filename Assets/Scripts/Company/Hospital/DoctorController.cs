using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorController : MonoBehaviour
{
    public Transform ButtonList;
    public NPCController npcController;
    public GameObject Hospital_Template;
    public QuestDialogue doctor_controller;

    GameObject Hospital;

    
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
        SetButtonOnDialogBox(1, "╬╨кам", () => OpenHospital(1));
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }

    private void OpenHospital(int n)
    {
        StatusEffectController player_effect = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<StatusEffectController>();
        int count = player_effect.GetstatusEffects().Count;
        if(count == 0)
        {
            // player Not has effect
            ButtonList.GetChild(n).gameObject.SetActive(false);
            npcController.RemoveText();
            npcController.IsEndSituation = false;
            npcController.dialogue.Clear();
            npcController.dialogue.Add(doctor_controller.greeting);
            npcController.dialogue.AddRange(doctor_controller.conversation);


            npcController.SetIndexConversation(0);
            npcController.StartDialogue();
            return;
        }
        Hospital = Instantiate(Hospital_Template, transform);
        npcController.dialoguePanel.SetActive(false);

        Timesystem time = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>();
        PlayerActivityController activity_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
        activity_controller.AddActivity(time.getDateTime(), UIHourActivity.acitivty_type.hospital);
        Hospital.gameObject.GetComponent<UIHospitalShow>().Show();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hospital.gameObject.GetComponent<UIHospitalShow>().Close();
        }
    }
}
