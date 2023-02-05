using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 4f;
    [SerializeField] float pickUpDistance = 2.5f;
    [SerializeField] float despawnTime = 10f;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime < 0)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (distance < 0.1f)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
                Debug.Log("Destroy");
            }
        }
    }

}
