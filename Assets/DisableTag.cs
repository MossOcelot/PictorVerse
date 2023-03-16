using UnityEngine;

public class DisableTag : MonoBehaviour
{
    private bool isColliding = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private void Update()
    {
        if (isColliding)
        {
            gameObject.tag = "ItemCutD";
        }
        else
        {
            gameObject.tag = "Enemy";
        }
    }
}
