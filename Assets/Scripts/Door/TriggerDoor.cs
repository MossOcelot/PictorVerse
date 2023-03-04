using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : DetectionZone
{
    public string DoorOpen = "DoorOpen";
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedObjs.Count > 0)
        {
            animator.SetBool(DoorOpen, true);

        }
        else
        {
            animator.SetBool(DoorOpen, false);

        }
    }
}
