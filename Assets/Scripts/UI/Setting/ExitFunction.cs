
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFunction : MonoBehaviour
{
    public void ExitGame() 
    {
       
       Application.Quit();
        
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
