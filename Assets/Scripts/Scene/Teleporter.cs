using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public bool IsHaveTicket;
    public Transform GetDestination()
    {
        return destination; 
    }
}
