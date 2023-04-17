using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNPC : MonoBehaviour
{
    public GameObject Player;
    private WinScripts winScripts;
    void Start()
    {
        winScripts = FindObjectOfType<WinScripts>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        Player.SetActive(false);
        if(winScripts.WinNow == true)
        {
            Player.SetActive(true);
            
        }
    }
}
