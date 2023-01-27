using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerManMovements : MonoBehaviour
{
    private Vector3 previousVelocity;

    private bool isMoving;

    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        
        anim.SetBool("isMoving", isMoving);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            previousVelocity = myRigidbody.velocity;
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetInteger("Direction", 2);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetInteger("Direction", 1);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetInteger("Direction", 0);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetInteger("Direction", 3);
            }

            isMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myRigidbody.constraints = RigidbodyConstraints2D.None;
            myRigidbody.velocity = previousVelocity;
            isMoving = false;

        }


    }

}

