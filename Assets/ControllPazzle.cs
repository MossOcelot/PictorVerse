using UnityEngine;

public class ControllPazzle : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectToToggle;


    private bool isCameraSmall = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isCameraSmall)
            {
                mainCamera.orthographicSize = 18f;
                isCameraSmall = true;
                GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = true;
                objectToToggle.SetActive(true);

            }


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.orthographicSize = 3.9f;
            isCameraSmall = false;
            GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = false;
            objectToToggle.SetActive(false);

        }
    }



}

