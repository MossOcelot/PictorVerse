using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float[] moveX;
    [SerializeField]
    private float[] moveY;
    private int waypointIndex = 0;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            animator.SetFloat("MoveX", moveX[waypointIndex]);
            animator.SetFloat("MoveY", moveY[waypointIndex]);

            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
}