using UnityEngine;

public class ControllPazzle : MonoBehaviour
{
    public Camera mainCamera; 
    public GameObject objectToToggle; 

    private bool isCameraSmall = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isCameraSmall)
            {
                mainCamera.orthographicSize = 18f; 
                isCameraSmall = true;
            }
            else
            {
                mainCamera.orthographicSize = 3.9f; 
                isCameraSmall = false;

            }

            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
