using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIFollow : MonoBehaviour
{

    
    public Vector3 position_player;
    public Vector3 firstposition_AI;
   
    public Vector3 position_spawner;
    private Vector3 directionVector;

    public float speed;
    public float distanceBetween;
    public bool isFollowPlayer;
    public GameObject spawner_area;
    public float radius;
    public float distance_spawner;
    private float distance;
    // Update is called once per frame
    private void Start()
    {
       
        
        firstposition_AI = gameObject.transform.position;
        radius = spawner_area.GetComponent<CircleCollider2D>().radius;
       
    }
    void Update()
    {
        followPlayer();
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            position_player = target.gameObject.transform.position; //praew
            isFollowPlayer = true;
        }
    }

    private void followPlayer()
    {

        distance = Vector2.Distance(transform.position, position_player);
        distance_spawner = Vector3.Distance(transform.position, position_spawner);

        Vector2 direction = position_player - transform.position;
        direction.Normalize();
        // ถ้าอยู่ในระยะตามเงื่อนไขให้ติดตามผู้เล่น
        /*
         เงื่อนไข
        1. ระยะผู้เล่น น้อยกว่าระยะที่กำหนดให้ monster ตามไป
        2. ระยะที่ศัตรูไม่เจอผู้เล่น เมื่อเดินถึงจุดสุดท้าย
        3. ระยะที่ออกห่างจาก จุด Spawn 
         */
        if (isFollowPlayer && distance != 0 && distance < distanceBetween && distance_spawner <= radius)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, position_player, speed * Time.deltaTime);
        }
        else
        {
            isFollowPlayer = false;
            position_player = new Vector3(0, 0, 0);
        }
        // ถ้าผู้เล่นไม่ได้อยู่ในระยะให้เดินกลับที่เดิม
        if (!isFollowPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, firstposition_AI, speed * Time.deltaTime);
        }
    }
    

    

}
