using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPage : MonoBehaviour
{
    [SerializeField]
    public GameObject cityMap;

    [SerializeField]
    public GameObject galaxyMap;

    public void close_window()
    {
        gameObject.SetActive(false);
    }

    public void open_window()
    {
        gameObject.SetActive (true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
