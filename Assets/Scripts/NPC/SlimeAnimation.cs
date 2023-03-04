using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isMoving = false;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            if (isMoving)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }

    public void SetMoving(bool moving)
    {
        isMoving = moving;
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("IsDeath");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
