using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{

    public float ChangeTime;
    public string sceneName;
    
    private void Update()
    {
        ChangeTime -= Time.deltaTime;
        if(ChangeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
