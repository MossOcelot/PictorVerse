using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChange : MonoBehaviour
{
    [SerializeField] private Vector2 desiredPosition;
    public GameObject player;


    public void ssChangeScene()
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        SceneManager.LoadScene("TutorialInspaceStation1");
        playerMovement.isLooking = false;

        player.transform.position = desiredPosition;

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

}
