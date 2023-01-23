using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Protal_Manager : MonoBehaviour
{
    private void Start()
    {
       GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = gameObject.transform.position;
                
        
    }
}
