using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject Progressshow;
    
        public void LoadGame()
        {
            StartCoroutine((IEnumerator)LoadGameAfterDelay());
        }
        private IEnumerator LoadGameAfterDelay()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(2);
        }
    
    public void Active()
    {
        Progressshow.SetActive(true);
    }

}
