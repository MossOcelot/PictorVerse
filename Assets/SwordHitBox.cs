using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 1f;

    public Collider2D swordCollider;

    void Start()
    {
        if(swordCollider == null)
        {
            Debug.LogWarning("Collider was not set");
        }
        swordCollider.GetComponent<Collider2D>();

    }

    void OncollisionEnter2D(Collision col)
    {
        col.collider.SendMessage("OnHit", swordDamage);
        }
}
