using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public CircleCollider2D area_spawn;
    public LayerMask layerMask;
    public int number_of_monster;
    private int count_monster;

    private int loss_monster;
    private Vector3 position;
    private Vector2 center;
    private float radius;

    public Vector2 Generate()
    {
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float distance = UnityEngine.Random.Range(0f, radius);
        float x = center.x + distance * Mathf.Cos(angle);
        float y = center.y + distance * Mathf.Sin(angle);
        return new Vector2(x, y);
    }
   

    public List<Collider2D> GetObjects()
    {
        List<Collider2D> objectsInArea = new List<Collider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, layerMask);
        foreach (Collider2D collider in colliders)
        {
            objectsInArea.Add(collider);
        }
        return objectsInArea;
    }
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.GetComponent<Transform>().position;
        center = new Vector2(position.x, position.y);
        radius = area_spawn.radius;

        for(int i = 0; i < number_of_monster ; i++)
        {
            Vector2 randomPoint = Generate();
            Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
        }
        Debug.Log("Hello");
    }

    private void Update()
    {
        remonster();
    }

    private void remonster()
    {
        List<Collider2D> objects = GetObjects();
        count_monster = objects.Count / 2;

        if (count_monster < number_of_monster)
        {
            loss_monster = number_of_monster - count_monster;
            for (int n = 0; n < loss_monster; n++)
            {
                Vector2 randomPoint = Generate();
                Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
            }
        }
    }
    // Update is called once per frame





}
