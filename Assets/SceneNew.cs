using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNew : MonoBehaviour
{
    public void MoveToScene()
    {
        SceneManager.LoadScene("TutorialInspaceStation1");
    }
}
