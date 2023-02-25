using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    void OnHit(float damage)
    {
        Debug.Log("Slime Hit" + damage);
    }
}
