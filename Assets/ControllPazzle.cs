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
    public NPCController npcController;
    



    void Start()
    {
        Menu = GameObject.FindGameObjectWithTag("Menu");
        winPuzzles = FindObjectOfType<WinScripts>();
        npcController = FindObjectOfType<NPCController>();

        if (winPuzzles.WinNow == true || npcController.isPuzzle == false)
        {
            Menu.SetActive(true);
            Map.SetActive(true);
            Status.SetActive(true);
            Mission.SetActive(true);
            mainCamera.orthographicSize = 3.9f;

        }
        if(npcController.isPuzzle == true)
        {
            Menu.SetActive(false);
            Map.SetActive(false);
            Status.SetActive(false);
            Mission.SetActive(false);
            mainCamera.orthographicSize = 18f;
        }





    }

}

