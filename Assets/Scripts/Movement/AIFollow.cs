using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIFollow : MonoBehaviour
{
    public Vector3 position_player;
    public Vector3 firstposition_AI;
   
    public Vector3 position_spawner;

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
        // �����������е�����͹����Դ���������
        /*
         ���͹�
        1. ���м����� ���¡������з���˹���� monster ����
        2. ���з���ѵ������ͼ����� ������Թ�֧�ش�ش����
        3. ���з���͡��ҧ�ҡ �ش Spawn 
         */
        if (distance < distanceBetween && distance != 0 && isFollowPlayer && distance_spawner <= radius)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, position_player, speed * Time.deltaTime);
        }
        else
        {
            isFollowPlayer = false;
            position_player = new Vector3(0, 0, 0);
        }
        // ��Ҽ�����������������������Թ��Ѻ������
        if (!isFollowPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, firstposition_AI, speed * Time.deltaTime);
        }
    }


}
