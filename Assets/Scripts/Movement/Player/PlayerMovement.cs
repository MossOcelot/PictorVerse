using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField]
    private PlayerStatus status;
    private Rigidbody2D rb;
    public Animator animator;

    public float walk_distance = 0;
    private bool iswalk;

    [SerializeField]
    private int energy_for_walk;
    Vector2 playerposition;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerposition = transform.position;
    }
    private void Update()
    {
        Movement();
        if (movement.x != 0 || movement.y != 0) { 
            iswalk = true;
        } else
        {
            iswalk = false;
        }
        count_distance_for_walk();
    }

    
    private void Movement()
    {
        int energy = status.getEnergy();
        if (energy == 0)
        {
            moveSpeed = 1f;
        } else
        {
            moveSpeed = 5f;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        AnimationMovement();
        
        
    }
    private void AnimationMovement()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Monster"))
        {
            Destroy(target.gameObject);
        }
    }

    private void count_distance_for_walk() {
        if(iswalk)
        {
           var distance = Vector2.Distance(transform.position, playerposition);
           if (distance >= 10)
            {
                walk_distance += 10;
                playerposition = transform.position;
                useEnergy(energy_for_walk);
            }
        }
    }

    private void useEnergy(int energy)
    {
        status.setEnergy(-energy);
        status.static_useEnergy += energy;
    }

}
