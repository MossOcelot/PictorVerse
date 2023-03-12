using UnityEngine;

public class FollowThePath : MonoBehaviour
{

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;
    private int waypointIndex = 0;
    public Animator animator;

    private Vector3 directionVector;

    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
        animator.SetFloat("MoveX", -8f);
        animator.SetFloat("MoveY", 8f);


    }
  

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {

            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;

            }

        }
    }
}