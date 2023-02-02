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

    void Start()
    {
        Hitpoints = MaxHitpoints;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }
        timeBetweenHits = 10f;
    }

    public override void Hit()
    {
        Hitpoints -= 1;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }

        if (Hitpoints <= 0)
        {
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

        timer = 0f;
        StartCoroutine(ResetHitpoints());
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
