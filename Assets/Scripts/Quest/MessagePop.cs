using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePop : MonoBehaviour
{
    public GameObject Message;
    public GameObject Player;
    public List<Vector2> positions;
    private int currentPositionIndex = 0;

    private void Start()
    {
        positions.Sort((p1, p2) => -p1.magnitude.CompareTo(p2.magnitude));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Message.SetActive(true);
            StartCoroutine(FreezePlayerPosition());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Message.SetActive(false);
            // Set new position of the gameObject
            if (currentPositionIndex < positions.Count - 1)
            {
                currentPositionIndex++;
            }
            else
            {
                // Destroy the game object if the last position has been reached
                Destroy(gameObject);
            }
            transform.position = positions[currentPositionIndex];
        }
    }

    IEnumerator FreezePlayerPosition()
    {
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSecondsRealtime(3f);
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
