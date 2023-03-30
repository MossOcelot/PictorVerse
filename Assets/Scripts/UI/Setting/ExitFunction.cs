#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFunction : MonoBehaviour
{
    public void ExitGame() 
    {
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        } else
        {
            Application.Quit();
        }
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
#endif