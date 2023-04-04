using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowingManager : MonoBehaviour
{
    public GameObject Banker;
    public PlayerStatus playerStatus;

    public void SetPlayer()
    {
        this.playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }

    public void Close()
    {
        Banker.gameObject.GetComponent<NPCController>().IsOpenShelf = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
