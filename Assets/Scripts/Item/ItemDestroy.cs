using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDestroy : Tool
{
    public string name_object;
    public QuestObjective.QuestObjectiveType type;
    [SerializeField]
    private AudioSource DestroySFX;
    [System.Serializable]
    public class ItemDropData
    {
        public Item item;
        public int Maxquantity;
        public float PercentDrop;
    }
    [SerializeField] private AIFollow aiFollow;
    [SerializeField] GameObject dropItem;
    [SerializeField] List<ItemDropData> item_datas;
    [SerializeField] int dropCount = 15;
    [SerializeField] float spread = 2f;
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public HealthBarBehavior HealthBar;
    private float timeBetweenHits;
    private float timer;
    public string playerTag = "Player";
    public Animator animator;
    private bool isDestroyed = false;
    private Rigidbody2D rb;
    public float knockbackForce;
    public Vector3 moveDirection;
    public GameObject floatingPoints;

    bool isDestroy;
    void Start()
    {
        Hitpoints = MaxHitpoints;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }
        timeBetweenHits = 10f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckCollision();
        if (Hitpoints <= 0)
        {
            if (!isDestroy)
            {

                GameObject.FindGameObjectWithTag("MissionQuest").gameObject.GetComponent<MissionCanvasController>().UpdateObjective(type, name_object, 1);
                isDestroy = true;
            }
            aiFollow.setIsDied(true);
        }
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Weapon"))
            {
                animator.SetTrigger("isAttack");
                Hit();
            }
        }
    }


    public override void Hit()
    {
        Hitpoints -= 1;

        moveDirection += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        rb.AddForce(moveDirection.normalized * -3000f);

        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }

        if (Hitpoints <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(DestroyAfterDelay());

        }
        else
        {
            timer = 0f;
            StartCoroutine(ResetHitpoints());
            animator.SetTrigger("isHurt");
            GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<TextMesh>().text = "-1";

        }
    }



    IEnumerator DestroyAfterDelay()
    {
        animator.SetTrigger("isDeath");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1f);

        int quantity_item = item_datas.Count;
        if (quantity_item == 0) quantity_item = 1;
        int quantity_drop = (int)Random.Range(1, quantity_item);
        Debug.Log("Random " + quantity_drop);
        dropCount = quantity_drop;
        while (dropCount > 0)
        {
            dropCount -= 1;
            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;

            GameObject go = Instantiate(dropItem);
            ItemDropData item = GetRandowmItem();

            if (item == null) continue;
            int quantity_itemDrop = (int)Random.Range(1, item.Maxquantity);
            go.GetComponent<ItemPickup>().SetItemPickUp(item.item, quantity_itemDrop);
            if (go == null) break;
            go.transform.position = pos;
            Debug.Log($"index: {dropCount} item: {item.item.item_name} quantity: {quantity_itemDrop}");
        }
        Destroy(gameObject);
        DestroySFX.Play();
    }

    private ItemDropData GetRandowmItem()
    {
        float perdrop = Random.value;
        foreach (ItemDropData item in item_datas)
        {
            float itemChance = (float)item.PercentDrop / 100;

            Debug.Log("itemChance: " + itemChance);
            if (itemChance > perdrop)
            {
                return item;
            }
        }
        return null;
    }

    IEnumerator ResetHitpoints()
    {
        yield return new WaitForSeconds(1f);
        while (timer < timeBetweenHits)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Hitpoints = MaxHitpoints;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        }
    }
}
