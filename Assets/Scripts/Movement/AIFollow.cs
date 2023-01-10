using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    public Vector3 position_player;
    public float speed;
    public float distanceBetween;
    private float distance;
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, position_player);
        Vector2 direction = position_player - transform.position;
        direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, position_player, speed * Time.deltaTime);

        }
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            position_player = target.gameObject.transform.position; //praew
        }

    }

}
