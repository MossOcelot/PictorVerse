using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public AnimatorOverrideController DefaultSkin;
    public AnimatorOverrideController DiggerSkin;

    public void DefaultSkinONE()
    {
        GetComponent<Animator>().runtimeAnimatorController = DefaultSkin as RuntimeAnimatorController;
    }


    public void DefaultSkinTWO()
    {
        GetComponent<Animator>().runtimeAnimatorController = DiggerSkin as RuntimeAnimatorController;
    }

    
}
