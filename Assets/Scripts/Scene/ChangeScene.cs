using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    FadeInOut fade;    
    public Transform Progessbar;

    public GameObject bar;
    public GameObject player;
    public int TimeOfChange;
    [SerializeField] private Vector2 desiredPosition;
    public bool CanNotOpen = false;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        player = GameObject.FindWithTag("Player");
        Progessbar.gameObject.SetActive(false);

    }

    public IEnumerator _ChangeScene()
    {
        Debug.Log("QQQ");
        fade.FadeIn();
        Debug.Log("AAA");
        yield return new WaitForSeconds(TimeOfChange);
        Debug.Log("BBB");
        SceneManager.LoadScene(sceneName);
        Debug.Log("CCC");
        yield return new WaitForEndOfFrame(); 
        player = GameObject.FindWithTag("Player");
        player.transform.position = desiredPosition; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && !CanNotOpen)
        {
            Progessbar.gameObject.SetActive(true);
            AnimateBar();
            StartCoroutine(_ChangeScene());

        }
    }
    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, TimeOfChange);

    }
}
