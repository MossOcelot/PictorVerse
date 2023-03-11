using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banker : MonoBehaviour
{
    public Transform ButtonList;
    public GameObject BankShelf;
    public GameObject InsuranceShelf;
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
        SetButtonOnDialogBox(1, "OpenInsurance", () => OpenInsurance());
        SetButtonOnDialogBox(2, "OpenBank", () => OpenBank());
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }
    public void OpenBank()
    {
        BankShelf.SetActive(true);
        BankShelf.gameObject.GetComponent<Bank_Status>().Banker = gameObject;
        npcController.playerIsClose = false;
        npcController.dialoguePanel.SetActive(false);
    }

    public void OpenInsurance()
    {
        CallInsurancePage insurance = GameObject.FindGameObjectWithTag("BankCompany").gameObject.GetComponent<CallInsurancePage>();
        UIInsurance uIInsurance = GameObject.FindGameObjectWithTag("Insurance").gameObject.GetComponent<UIInsurance>();

        uIInsurance.insurance_manager.Banker = gameObject;
        insurance.SetInsurance();
        InsuranceShelf.SetActive(true);
        uIInsurance.UpdateUIInsurance();
        npcController.playerIsClose = false;
        npcController.dialoguePanel.SetActive(false);
    }
}
