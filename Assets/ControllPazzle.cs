using UnityEngine;


public class ControllPazzle : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectToToggle;
    public GameObject Menu;
    public GameObject Map;
    public GameObject Status;
    public GameObject Mission;
    public WinScripts winPuzzles;


    private bool isCameraSmall = false;

    void Start()
    {
        Menu = GameObject.FindGameObjectWithTag("Menu");
        winPuzzles = FindObjectOfType<WinScripts>();

        if (winPuzzles.WinNow == true)
        {
            isCameraSmall = true;

        }
        if (isCameraSmall == false)
        {
            Menu.SetActive(false);
            Map.SetActive(false);
            Status.SetActive(false);
            Mission.SetActive(false);
            mainCamera.orthographicSize = 3.9f;
            objectToToggle.SetActive(true);
        }
        if (isCameraSmall == true)
        {
            Menu.SetActive(true);
            Map.SetActive(true);
            Status.SetActive(true);
            Mission.SetActive(true);
            mainCamera.orthographicSize = 18f;
            objectToToggle.SetActive(false);

        }
    }






}

