using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMonsterAttack : MonoBehaviour
{
    private bool canKnockback = true;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (canKnockback && (other.gameObject.CompareTag("ItemCutD") || canKnockback && (other.gameObject.CompareTag("Enemy"))))
        {
            Vector2 knockbackDirection = (transform.position - other.gameObject.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 1500, ForceMode2D.Impulse);
            StartCoroutine(FlashRed());
            canKnockback = false;
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(2f);
        canKnockback = true;
    }

    public IEnumerator FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ItemCutD") || other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
