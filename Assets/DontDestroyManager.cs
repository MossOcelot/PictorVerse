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
            string tag = obj.tag;
            int len_obj = GameObject.FindGameObjectsWithTag(tag).Length;
            Debug.Log($"tag: {tag} len: {len_obj}");
            foreach(GameObject obj2 in GameObject.FindGameObjectsWithTag(tag)) 
            {
                Debug.Log($"transform obj: {obj2.name} {obj2.transform.position}");
            }
            if (len_obj > 1 )
            {
                Destroy(obj);
            }
            DontDestroyOnLoad(obj);
        }
    }
}
