using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs;
    private void Awake()
    {
        // DontDestroyOnLoad(this);
        foreach(GameObject obj in objs)
        {
            string tag = obj.tag;
            int len_obj = GameObject.FindGameObjectsWithTag(tag).Length;
            if (len_obj > 1 )
            {
                bool status = false;
                foreach(GameObject obj2 in GameObject.FindGameObjectsWithTag(tag))
                {
                    SceneStatus scene = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
                    if (obj2.name == "Goverment")
                    {
                        if(obj2.gameObject.GetComponent<GovermentStatus>().govermentInSection.ToString() != scene.sceneInsection.ToString())
                        {
                            Destroy(obj2.gameObject);
                            status = true;
                            break;
                        }
                    } else if (obj2.name == "central bank")
                    {
                        if (obj2.gameObject.GetComponent<CentralBankStatus>().govermentInSection.ToString() != scene.sceneInsection.ToString())
                        {
                            Destroy(obj2.gameObject);
                            status = true;
                            break;
                        }
                    }
                }
                if (status)
                {
                    DontDestroyOnLoad(obj);
                    continue;
                }
                Destroy(obj);
            }
            DontDestroyOnLoad(obj);
        }
    }
}
