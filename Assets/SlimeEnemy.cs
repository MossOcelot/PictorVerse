using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour, IEnemy
{
    public int ID { get; set; }
    public Monster_Status monsterStatus;
    public int damageAmount;
    public float knockbackForce = 100f;

    private void Start()
    {
        ID = 0;
        damageAmount =  monsterStatus.atk;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
            playerStatus.setHP(-damageAmount);

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = collision.contacts[0].normal;
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }
}
