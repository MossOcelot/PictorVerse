using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{

    public float ChangeTime;
    public string sceneName;
    public GameObject player;
    [SerializeField] private Vector2 desiredPosition;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    private void Update()
    {
        ChangeTime -= Time.deltaTime;
        if(ChangeTime <= 0)
        {
            PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();

            playerMovement.isLooking = false;
            player.transform.position = desiredPosition;
            SceneManager.LoadScene(sceneName);
            player.transform.position = desiredPosition;

        }
    }
}
