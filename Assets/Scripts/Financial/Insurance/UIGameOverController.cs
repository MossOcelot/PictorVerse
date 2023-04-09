using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class UIGameOverController : MonoBehaviour
{
    PlayerStatus playerStatus;
    public GameObject UI;
    public GameObject Page1;
    public GameObject Page2;
    bool ActiveObj;
    private void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }
    // Update is called once per frame
    private void Update()
    {
        bool IsDead = playerStatus.IsDead;
        if(IsDead && !ActiveObj)
        {
            
            StartCoroutine(OpenUI(5));
            ActiveObj = true;
        }
    }

    private IEnumerator OpenUI(int time)
    {

        yield return new WaitForSeconds(time);
        UI.SetActive(true);
    }
    public void Close()
    {
        Page2.SetActive(false);
        Page1.SetActive(true); 
        ActiveObj = false;
        UI.SetActive(false);
    }
}
