using UnityEngine;

public class MoveSystems : MonoBehaviour
{
    [SerializeField] private GameObject correctForm = null;
    [SerializeField] private float snapDistance = 0.5f;

    private bool isMoving = false;
    public bool isFinished = false;
    private Vector3 resetPosition = Vector3.zero;
    private Vector3 initialMouseOffset = Vector3.zero;

    private void Awake()
    {
        resetPosition = transform.localPosition;
    }

    private void Update()
    {
        if (!isFinished && isMoving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = mousePosition - initialMouseOffset;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialMouseOffset = mousePosition - transform.localPosition;
            isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if (!isFinished)
        {
            Vector3 correctPosition = correctForm.transform.position;
            Vector3 currentPosition = transform.position;

            if (Vector3.Distance(currentPosition, correctPosition) <= snapDistance)
            {
                transform.position = correctPosition;
                isFinished = true;
                FindObjectOfType<WinScripts>()?.AddPoints();
            }
            else
            {
                transform.localPosition = resetPosition;
            }
        }
    }
}
