using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 3f; //initial distance of interact
    public Transform player;
    public Transform interactItem;
    bool hasInteract = false; // check if touching an item

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, interactItem.position);
        if (distance <= radius && !hasInteract && Input.GetKeyDown(KeyCode.F))
        {
            hasInteract = true;
            Interact();
        }
    }

    public virtual void Interact()
    {

    }
    //create pick up area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactItem.position, radius);
    }
}
