using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : DetectionZone
{
    public ChangeScene changeScene;
    public string DoorOpen = "DoorOpen";
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        changeScene = gameObject.GetComponent<ChangeScene>();
        if(changeScene == null)
        {
            if (detectedObjs.Count > 0)
            {
                animator.SetBool(DoorOpen, true);

            }
            else
            {
                animator.SetBool(DoorOpen, false);

            }
        } else
        {
            if (detectedObjs.Count > 0 && !changeScene.CanNotOpen)
            {
                animator.SetBool(DoorOpen, true);

            }
            else
            {
                animator.SetBool(DoorOpen, false);

            }
        }
        
    }
}
