using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovements : MonoBehaviour
{

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
    private void nCollisionEnter2D(Collision2D other)
    {
        Vector3 temp = directionVector;
        ChangeDirection();

        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }
}

//MoveX = Horizontal
//MoveY = Vertical