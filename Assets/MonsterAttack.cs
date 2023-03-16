using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private PlayerStatus Player;
    [SerializeField] private Monster_Status monsterstatus;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        Monster_Status monsterStatus = GetComponent<Monster_Status>();
        int attackValue = monsterStatus.GetAttack();

        if (other.gameObject.CompareTag("Player"))
        {
            Player.setHP(-attackValue);
            Debug.Log(attackValue);
        }
    }
}
