using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMonsterAttack : MonoBehaviour
{
    public PlayerStatus status;
    public StatusEffectController player_effects;

    public List<StatusEffectPlayer> effect_to_work;
    private bool canKnockback = true;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (canKnockback && (other.gameObject.CompareTag("ItemCutD") || canKnockback && (other.gameObject.CompareTag("Enemy"))))
        {
            chanceOfGettingSick();
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

    private void chanceOfGettingSick()
    {
        float rand_chance = UnityEngine.Random.Range(0f, 100f);
        float chance = CalculateChanceSick();
        if (rand_chance <= chance)
        {
            int StatusIndex = Random.Range(0, (effect_to_work.Count - 1));
            StatusEffectPlayer effect = effect_to_work[StatusIndex];
            player_effects.AddStatus(effect);
        }
    }

    private float CalculateChanceSick()
    {
        float healthy = status.getMyStatic().static_healthy;
        float chance = 0.1f;
        if (healthy <= 20)
        {
            chance *= 5f;
        }
        else if (healthy <= 80)
        {
            chance *= 2.5f;
        }
        else if (healthy <= 100)
        {
            chance *= 1;
        }
        else
        {
            chance *= 0.5f;
        }
        return chance;
    }
}
