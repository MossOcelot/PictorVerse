using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePortal : MonoBehaviour
{
    public Scene scene;
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene.name);
        }       
    }
}
