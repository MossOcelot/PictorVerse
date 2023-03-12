using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    FadeInOut fade;
    public GameObject player;

    [SerializeField] private Vector2 desiredPosition;
    public bool CanNotOpen = false;
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        player = GameObject.FindWithTag("Player");
    }

    public IEnumerator _ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForEndOfFrame(); 
        player = GameObject.FindWithTag("Player");
        player.transform.position = desiredPosition; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !CanNotOpen)
        {
            StartCoroutine(_ChangeScene());
        }
    }
}
