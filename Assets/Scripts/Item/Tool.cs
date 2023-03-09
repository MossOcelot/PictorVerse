 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public virtual void Hit()
    {
        if (gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Compare with Weapon Tag");
        }
        
    }

}
