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
    public GameObject LoanShelf;
    public NPCController npcController;

    public void Update()
    {
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
        SetButtonOnDialogBox(1, "OpenInsurance", () => OpenInsurance());
        SetButtonOnDialogBox(2, "OpenBank", () => OpenBank());
        Debug.Log("AA");
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
        BankShelf.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        BankShelf.gameObject.GetComponent<Bank_Status>().Banker = gameObject;
        //npcController.playerIsClose = false;
        npcController.dialoguePanel.SetActive(false);
    }

    public void OpenInsurance()
    {
        Debug.Log("OpenInsurance");
        CallInsurancePage insurance = GameObject.FindGameObjectWithTag("BankCompany").gameObject.GetComponent<CallInsurancePage>();
        UIInsurance uIInsurance = GameObject.FindGameObjectWithTag("Insurance").gameObject.GetComponent<UIInsurance>();

        uIInsurance.insurance_manager.Banker = gameObject;
        Debug.Log("OI1");
        insurance.SetInsurance();
        Debug.Log("OI2");
        InsuranceShelf.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("OI3");
        uIInsurance.UpdateUIInsurance();
        Debug.Log("OI4");
        //npcController.playerIsClose = false;
        npcController.dialoguePanel.SetActive(false);
        Debug.Log("OI5");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BankShelf.gameObject.GetComponent<Bank_Status>().Close();
            LoanShelf.gameObject.GetComponent<BorrowingManager>().Close();
            InsuranceShelf.gameObject.GetComponent<UIInsurance>().Close();
        }
    }
}
