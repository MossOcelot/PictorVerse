using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private PlayerStatus Player;
    private StatusEffectController player_effects;
    [SerializeField] private Monster_Status monsterstatus;

    [SerializeField] private List<StatusEffectPlayer> status_effects;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        player_effects = GameObject.FindWithTag("Player").GetComponent<StatusEffectController>();
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
            chanceOfGettingSick();
            Debug.Log(attackValue);
        }
    }

    private void chanceOfGettingSick()
    {
        float rand_chance = UnityEngine.Random.Range(0f, 100f);
        float chance = CalculateChanceSick();
        if (rand_chance <= chance)
        {
            int StatusIndex = Random.Range(0, (status_effects.Count - 1));
            StatusEffectPlayer effect = status_effects[StatusIndex];
            player_effects.AddStatus(effect);
        }
    }

    private float CalculateChanceSick()
    {
        float healthy = Player.getMyStatic().static_healthy;
        float chance = 0.5f;
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
