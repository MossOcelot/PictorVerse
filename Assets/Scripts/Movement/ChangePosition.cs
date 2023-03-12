using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bank")
        {
            animator.Play("Idle nw");
        }
    }
}
