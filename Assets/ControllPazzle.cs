using UnityEngine;

public class ControllPazzle : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectToToggle;

    private bool isCameraSmall = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isCameraSmall)
            {
                mainCamera.orthographicSize = 18f;
                isCameraSmall = true;
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().SetIsLooking(true);
                GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = true;
                objectToToggle.SetActive(true);

            }


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCamera.orthographicSize = 3.9f;
            isCameraSmall = false;
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().isLooking = false;
            GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = false;
            objectToToggle.SetActive(false);

        }
    }



}

