using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistObject : MonoBehaviour
{
    private Vector3 savedPosition;
    private float savedHealth;
    private List<string> savedInventory;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single)
        {
            transform.position = savedPosition;
            // set health, inventory, etc. to saved values
        }
    }

    private void OnApplicationQuit()
    {
        savedPosition = transform.position;
        // save health, inventory, etc.
    }

    public void Save()
    {
        savedPosition = transform.position;
        // save health, inventory, etc.
    }
}
