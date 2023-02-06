using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        foreach(GameObject obj in objs)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
