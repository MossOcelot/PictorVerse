using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    public GameObject[] spawnToObject;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in spawnToObject)
        {
            Instantiate(objectToSpawn, obj.transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
