using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketCheckMachine : MonoBehaviour
{
    public TriggerDoor door;
    public Transform ButtonList;
    public GameObject TicketShelf;
    public NPCController npcController;

    public void Update()
    {
        bool IsEndSituation = npcController.IsEndSituation;
        Debug.Log("IsEnd: " + IsEndSituation);  
        if (!IsEndSituation)
        {
            ButtonList = GameObject.FindGameObjectWithTag("DialogBox").gameObject.transform.GetChild(3).gameObject.transform;
            return;
        }
        if (IsEndSituation && npcController.playerIsClose)
        {
            ButtonList = GameObject.FindGameObjectWithTag("DialogBox").gameObject.transform.GetChild(3).gameObject.transform;
            AddButtonInDialog();
        }
    }

    public void AddButtonInDialog()
    {
        SetButtonOnDialogBox(1, "UseTicket", () => CheckTicket());
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        Debug.Log("SetButton");
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }

    private void CheckTicket()
    {
        TicketInspace ticket = npcController.player.gameObject.GetComponent<TicketController>().GetTicket();
        if(ticket.LeaveStation != "")
        {
            Debug.Log("LeaveStation: " + ticket.LeaveStation);
            door.changeScene.sceneName = ticket.LeaveStation;
            door.changeScene.CanNotOpen = false;
            npcController.dialoguePanel.SetActive(false);
            // delete ticket
            npcController.player.gameObject.GetComponent<TicketController>().AddTicket(new TicketInspace());
        }
    }
}
