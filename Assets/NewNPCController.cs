using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NewNPCController : MonoBehaviour
{
    public NPCController npcController;
    public Transform ButtonList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("DialogBox").gameObject != null)
        {
            ButtonList = GameObject.FindGameObjectWithTag("DialogBox").gameObject.transform;
        }
        return;
    }

    public void AddButtonInDialog()
    {
        SetButtonOnDialogBox(1, "change scene", () => ChangeScenes());
    }

    public void SetButtonOnDialogBox(int n, string nameBtn, Action action)
    {
        Debug.Log("SetButton");
        ButtonList.GetChild(n).gameObject.SetActive(true);
        ButtonList.GetChild(n).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = nameBtn;
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        ButtonList.GetChild(n).gameObject.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public void ChangeScenes()
    {
        SceneManager.LoadScene("Bank");
    }
}
