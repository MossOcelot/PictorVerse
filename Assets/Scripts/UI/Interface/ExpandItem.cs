using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public Transform trans;

    private void Awake()
    {
        //img = GetComponent<Image>();
        //trans = transform;
    }
}
