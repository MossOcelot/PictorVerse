using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float AttackRangeX;
    public float AttackRangeY;
    public int damage;
    public Animator playerAnim;

    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                playerAnim.SetTrigger("SwordAttack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,new Vector2 (AttackRangeX , AttackRangeY), 0 ,whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPos.position,new Vector3(AttackRangeX,AttackRangeY,1));
        }
    }
}
