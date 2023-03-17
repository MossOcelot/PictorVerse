using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObjectPosition : MonoBehaviour
{
    public Transform Object;
    public Transform PointObject;
    public Transform anotherpointer;
    public List<Vector2> positions = new List<Vector2>();
    private int currentPositionIndex = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentPositionIndex < positions.Count) 
            {
                Object.position = positions[currentPositionIndex]; 
                currentPositionIndex++; 
            }
            else
            {
                anotherpointer.gameObject.SetActive(false);
                PointObject.gameObject.SetActive(false);
                Object.gameObject.SetActive(false);
                //Destroy(gameObject);

               
            }
            
        }
    }
}
