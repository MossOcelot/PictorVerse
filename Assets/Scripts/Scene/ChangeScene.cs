using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    FadeInOut fade;    
    //public Transform Progessbar;

    //public GameObject bar;
    public GameObject player;
    //public int TimeOfChange;
    [SerializeField] private Vector2 desiredPosition;
    public bool CanNotOpen = false;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        player = GameObject.FindWithTag("Player");
        //Progessbar.gameObject.SetActive(false);

    }

    public IEnumerator _ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1f);

        Timesystem time = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>();
        SceneManager.LoadScene(sceneName);
        if(sceneName == "Bank")
        {
            
            PlayerActivityController activity_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
            activity_controller.AddActivity(time.getDateTime(), UIHourActivity.acitivty_type.bank);
        } else if(sceneName == "Cave")
        {
            PlayerActivityController activity_controller = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerActivityController>();
            activity_controller.AddActivity(time.getDateTime(), UIHourActivity.acitivty_type.Mining);
        }
        yield return new WaitForEndOfFrame(); 
        player = GameObject.FindWithTag("Player");
        player.transform.position = desiredPosition; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && !CanNotOpen)
        {
            //Progessbar.gameObject.SetActive(true);
            //AnimateBar();
            if (sceneName == "Inspace station") return; // fix bug в§иж 
            StartCoroutine(_ChangeScene());

        }
    }
    //public void AnimateBar()
    //{
        //LeanTween.scaleX(bar, 1, TimeOfChange);

    //}
}
