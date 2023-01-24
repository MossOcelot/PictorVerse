using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    // กำหนด LayerMask
    public LayerMask interactableLayer;

    public float defaultMoveSpeed = 5f;
    private float moveSpeed;
    private bool isMoving;


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
    bool isTyping;
    private void Start()
    {
        moveSpeed = defaultMoveSpeed;
        rb = GetComponent<Rigidbody2D>();
        playerposition = transform.position;
    }


    private void Update()
    {
        //กำหนดการหยุดของ layerMask
        if (IsWalkable(movement))
        {
            Movement();
        }
        if (movement.x != 0 || movement.y != 0)
        {
            iswalk = true;
            isMoving = true;
            isTyping = true;
        }
        else
        {
            iswalk = false;
            isMoving = false;
            isTyping = false;
        }
        count_distance_for_walk();
        //Set การหยุดหรือไม่หยุดของ Animation
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();

        
    }


    private void Movement()
    {
        int energy = status.getEnergy();
        if (energy == 0)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //หันหน้า
        if (movement != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);

        }
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
        if (iswalk)
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

    //เซ็ท ห้ามให้ player เข้าเมื่อเจอ IsWalkable
    private bool IsWalkable(Vector3 movement)
    {
        if (Physics2D.OverlapCircle(movement, 0.3f, interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        // Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);

        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
}
