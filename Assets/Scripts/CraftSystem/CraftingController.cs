using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    public Transform ButtonList;
    public GameObject CraftTable;
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
        SetButtonOnDialogBox(1, "OpenCraftTable", () => OpenCraftTable());
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }

    private void OpenCraftTable()
    {
        CraftTable.SetActive(true);
        Debug.Log("OpenCraft1");
        CraftTable.GetComponent<UICraftingManager>().Open();
        npcController.dialoguePanel.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CraftTable.GetComponent<UICraftingManager>().Close();
            CraftTable.SetActive(false);
        }
    }
}
