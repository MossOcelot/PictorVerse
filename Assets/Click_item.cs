using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_item : MonoBehaviour
{
    [SerializeField]
    private Item_detail item_detail;

    
    public void getData()
    {
          
            Debug.Log(item_detail.Item_name);

    }
}
