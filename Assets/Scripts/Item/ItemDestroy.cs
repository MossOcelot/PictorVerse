using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : Tool
{
    [SerializeField] GameObject drop;
    [SerializeField] int dropCount = 15;
    [SerializeField] float spread = 2f;
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public HealthBarBehavior HealthBar;
    private float timeBetweenHits;
    private float timer;
    public string playerTag = "Player";
    public Animator animator;
    private bool isDestroyed = false;
    private Rigidbody2D rb;

    void Start()
    {
        Hitpoints = MaxHitpoints;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }
        timeBetweenHits = 10f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckCollision();
        if (Hitpoints <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Debug.Log("Enter");
        }
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player collided with Item");
                animator.SetTrigger("isAttack");
            }
            else
            {
                print("NO");
            }
        }
    }

    public override void Hit()
    {
        Hitpoints -= 1;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }

        if (Hitpoints <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            timer = 0f;
            StartCoroutine(ResetHitpoints());
            animator.SetTrigger("isHurt");
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        animator.SetTrigger("isDeath");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(5f);

        while (dropCount > 0)
        {
            dropCount -= 1;
            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(drop);
            if (go == null)
            {
                break;
            }
            go.transform.position = pos;
        }

        Destroy(gameObject);
    }

    IEnumerator ResetHitpoints()
    {
        while (timer < timeBetweenHits)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Hitpoints = MaxHitpoints;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }
    }

   
}
