using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObjectPosition : MonoBehaviour
{
    public Transform Object;
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
                Object.gameObject.SetActive(false);
            }
        }
    }
}
