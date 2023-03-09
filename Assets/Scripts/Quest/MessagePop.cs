using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePop : MonoBehaviour
{
    public List<GameObject> messages;
    public GameObject player;
    public List<Vector2> positions;
    private int currentPositionIndex = 0;
    private int currentMessageIndex = 0;

    private void Start()
    {
        positions.Sort((p1, p2) => -p1.magnitude.CompareTo(p2.magnitude));
        DisableAllMessages();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FreezePlayerPosition());
            messages[currentMessageIndex].SetActive(true);

        }
        else
        {
            messages[currentMessageIndex].SetActive(false);
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (currentPositionIndex < positions.Count - 1)
            {
                currentPositionIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
            transform.position = positions[currentPositionIndex];
            if (currentMessageIndex < messages.Count - 1)
            {
                messages[currentMessageIndex].SetActive(false);
                currentMessageIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator FreezePlayerPosition()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSecondsRealtime(3f);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void DisableAllMessages()
    {
        foreach (GameObject message in messages)
        {
            message.SetActive(false);
        }
    }
}
