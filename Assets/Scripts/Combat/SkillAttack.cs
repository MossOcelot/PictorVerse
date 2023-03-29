using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    [SerializeField]
    private AudioSource AttackSFX;

    public Animator animator;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("ItemCutD") || collider.gameObject.CompareTag("DT"))
            {
                AttackSFX.Play();
                animator.SetTrigger("SwordAttack");
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
        }
    }
}