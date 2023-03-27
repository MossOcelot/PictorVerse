using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowingManager : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public void SetPlayer()
    {
        this.playerStatus = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
