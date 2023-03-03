using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public CircleCollider2D area_spawn;
    public LayerMask layerMask;
    public int number_of_monster;
    public float respawn_delay = 5f;
    public int max_respawns = 10;

    private int count_monster;
    private int loss_monster;
    private Vector3 position;
    private Vector2 center;
    private float radius;
    private float last_respawn_time;
    private int num_respawns;

    void Start()
    {
        position = gameObject.GetComponent<Transform>().position;
        center = new Vector2(position.x, position.y);
        radius = area_spawn.radius;

        GameObject monster = objectToSpawn;
        monster.GetComponent<AIFollow>().position_spawner = gameObject.GetComponent<Transform>().position;
        monster.GetComponent<Monster_Status>().birthplace = gameObject.name;
        for (int i = 0; i < number_of_monster; i++)
        {
            Vector2 randomPoint = Generate();
            Instantiate(monster, randomPoint, Quaternion.identity);
        }
        last_respawn_time = Time.time;
        num_respawns = 0;
    }

    private void Update()
    {
        if (num_respawns < max_respawns && Time.time - last_respawn_time > respawn_delay)
        {
            last_respawn_time = Time.time;
            remonster();
        }
    }

    public Vector2 Generate()
    {
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float distance = UnityEngine.Random.Range(0f, radius);
        float x = center.x + distance * Mathf.Cos(angle);
        float y = center.y + distance * Mathf.Sin(angle);
        return new Vector2(x, y);
    }


    public int countObject()
    {
        int number_monsterInArea = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, layerMask);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Monster_Status>().birthplace == gameObject.name)
            {
                number_monsterInArea += 1;
            }
        }
        return number_monsterInArea;
    }

    private void remonster()
    {
        count_monster = countObject() / 2;

        if (count_monster < number_of_monster)
        {
            loss_monster = number_of_monster - count_monster;
            for (int n = 0; n < loss_monster; n++)
            {
                Vector2 randomPoint = Generate();
                Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
            }
            num_respawns++;
        }
    }
}
