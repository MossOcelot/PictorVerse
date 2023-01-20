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
            _ = StartCoroutine(FadeTo(0.15f, 0.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(FadeTo(1f, 0.5f));
        }
    }

    IEnumerator FadeTo(float targetOpacity, float duration)
    {
        // ถ้า sprite renderer ไม่ null ก่อนเข้าถึงสีของมัน
        if (spriteRenderer != null)
        {
            // รับค่า สีปัจจุบัน
            Color color = spriteRenderer.color;

            // คำนวณว่าจะเปลี่ยน opacity ต่อเฟรมเท่าไหร่
            float changePerSecond = (targetOpacity - color.a) / duration;
            // เริ่ม fade สี
            while (Mathf.Abs(color.a - targetOpacity) > 0.01f)
            {
                color.a += changePerSecond * Time.deltaTime;
                spriteRenderer.color = color;
                yield return null;
            }
            // เช็ค opacity
            color.a = targetOpacity;
            spriteRenderer.color = color;
        }
    }
}
