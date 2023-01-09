using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OtherCameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;


    //กำหนดความยาวช่วงของ smooth camera
    [Range(0,1)]
    public float smoothTime ;

    public Vector3 positionOffset;

    private void Awake()
    {

        //ทำงานใน Tag Player
        target = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void LateUpdate()
    {

        //จัดการมุมกล้อง
        Vector3 targetposition = target.position+positionOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
