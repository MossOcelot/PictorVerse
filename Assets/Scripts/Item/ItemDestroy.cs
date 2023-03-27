using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : Tool
{
    [SerializeField]
    private AudioSource DestroySFX;
    [SerializeField] private AIFollow aiFollow;
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
    public float knockbackForce;
    public Vector3 moveDirection;
    public GameObject floatingPoints;


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
            aiFollow.setIsDied(true);
        }
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Weapon"))
            {
                animator.SetTrigger("isAttack");
                Hit();
            }
        }
    }


    public override void Hit()
    {
            Hitpoints -= 1;

            moveDirection += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
            rb.AddForce(moveDirection.normalized * -3000f);

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
                GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "-1";

        }
    }
    


    IEnumerator DestroyAfterDelay()
    {
        animator.SetTrigger("isDeath");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1f);

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
        DestroySFX.Play();
        Destroy(gameObject);
    }

    IEnumerator ResetHitpoints()
    {
        yield return new WaitForSeconds(1f); 
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
