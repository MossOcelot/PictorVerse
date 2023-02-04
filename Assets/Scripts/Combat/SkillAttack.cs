using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("ItemCutD"))
            {
                Debug.Log("Player collided with Item");
                animator.SetTrigger("SwordAttack");
            }
            else
            {
                print("NO");
            }
        }
    }
}
