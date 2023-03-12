using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backButton : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    public void backing()
    {
        Panel.SetActive(true);
    }
}
