using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstopPlayer : MonoBehaviour
{
    public GameObject player;
    public float timeToremove;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Unstopplayer", timeToremove);

    }
    private void Unstopplayer()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
