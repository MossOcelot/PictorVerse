using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTicketSeller : MonoBehaviour
{
    public Transform ButtonList;
    public GameObject TicketShelf;
    public NPCController npcController;

    public void Update()
    {
        bool IsEndSituation = npcController.IsEndSituation;

        if (!IsEndSituation)
        {
            ButtonList = GameObject.FindGameObjectWithTag("DialogBox").gameObject.transform.GetChild(3).gameObject.transform;
            return;
        }
        if (IsEndSituation && npcController.playerIsClose)
        {
            AddButtonInDialog();
        }
    }

    public void AddButtonInDialog()
    {
        SetButtonOnDialogBox(1, "OpenShelf", () => OpenTicketShelf(1));
    }

    public void SetButtonOnDialogBox(int n,string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }
    public void OpenTicketShelf(int n)
    {
        TicketShelf.SetActive(true);
        TicketShelf.gameObject.GetComponent<BuyTicketShelfManager>().NPC = gameObject;
        npcController.playerIsClose = false;
        npcController.dialoguePanel.SetActive(false);
    }
}
