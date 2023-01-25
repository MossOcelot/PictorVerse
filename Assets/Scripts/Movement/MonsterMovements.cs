using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovements : MonoBehaviour
{
    private Vector3 previousVelocity;

    private bool isMoving;

    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bounds;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    private void Update()
    {
        Move();
        anim.SetBool("isMoving", isMoving);

    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                //ขวา
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                //บน
                break;
            case 2:
                directionVector = Vector3.left;
                //ซ้าย
                break;
            case 3:
                directionVector = Vector3.down;
                //ล่าง
                break;
            default:
                break;

        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
        anim.SetFloat("Speed", directionVector.sqrMagnitude);
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
                //Debug.Log("Now key code => W");
                anim.SetInteger("Direction", 2);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("Now key code => A");
                anim.SetInteger("Direction", 1);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //Debug.Log("Now key code => S");
                anim.SetInteger("Direction", 0);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //Debug.Log("Now key code => D");
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

