using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControlInterface : MonoBehaviour
{
    public GameObject Bag;
    public GameObject noti;
    public PlayerMovement playerMovement;
    public interfaceCanvasController main_camera;

    private string old_scene;
    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
        main_camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>();
    }

    private void Update()
    {
        if (playerMovement.isLooking)
        {
            Bag.SetActive(false);
        } else
        {
            Bag.SetActive(true);
        }

        if (main_camera.isLooking)
        {
            noti.SetActive(false);
        }
        else
        {
            noti.SetActive(true);   
        }
    }

    private void FixedUpdate()
    {
        string present_scene = SceneManager.GetActiveScene().name;

        if(present_scene != old_scene)
        {
            main_camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>();
            old_scene = present_scene;
        }
    }
}
