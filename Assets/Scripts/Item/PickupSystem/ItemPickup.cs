using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [field: SerializeField]
    public Item InventoryItem { get; private set; }
    [field: SerializeField]
    public int Quantity { get; set; } = 1;
    // พื้นที่ไว้ใส่เสียง

    [SerializeField]
    private float dulation = 0.3f;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = InventoryItem.icon;
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(1f, 1f);
    }

    internal void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        // ที่ play audio
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while(currentTime < dulation ) 
        { 
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / dulation);
            yield return null;
        }
        Destroy(gameObject);
    }
}
