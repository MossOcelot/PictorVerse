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

  void Start()
   {
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
   }

    public override void Hit()
    {
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        Hitpoints -= 1;

        if (Hitpoints <= 0)
        {
            while (dropCount > 0)
            {
            dropCount -= 1;
            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(drop);
            go.transform.position = pos;
        }
            Destroy(gameObject);

        }
    }

}
