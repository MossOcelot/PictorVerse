using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControlInterface : MonoBehaviour
{
    public GameObject Bag;
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.isLooking)
        {
            Bag.SetActive(false);
        } else
        {
            Bag.SetActive(true);
        }
    }
}
