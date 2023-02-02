using UnityEngine;
using System.Collections;


public class FadeOnCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public LayerMask playerLayer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(FadeTo(0.25f, 0.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(FadeTo(1f, 0.5f));
        }
    }

    private IEnumerator FadeTo(float targetOpacity, float duration)
    {
        Color startColor = spriteRenderer.color;
        float startOpacity = startColor.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float opacity = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, opacity);
            yield return null;
        }
    }
}

