using UnityEngine;

public class Sun : MonoBehaviour
{
    public float speed = 1f; // adjust this in the Inspector to control rotation speed
    public float radius = 5f; // adjust this in the Inspector to control the distance from the center
    public Color color = new Color(1f, 1f, 1f, 0f); // adjust this in the Inspector to change the color of the sun
    public Transform cameraTransform; // the camera's transform

    void Start()
    {
        // set the color of the sun's sprite
        GetComponent<SpriteRenderer>().color = color;
        // cache the camera transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // calculate the angle based on the speed and time
        float angle = Time.time * speed;
        // calculate the new position of the sun
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        // add the position of the camera to the new position
        newPos += cameraTransform.position;
        // set the position of the sun
        transform.position = newPos;
    }
}
