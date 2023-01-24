using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam,Dialog}

public class GameController : MonoBehaviour
{

    [SerializeField] PlayerMovement playercontroller;
     
    GameState state;
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
            Debug.Log("OnshowDialog");
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {

            Debug.Log("OnCloseDialog");
            //if (state = GameState.FreeRoam)
            {
                Debug.Log("Helloworld");

            }

        };
    }

    private void Update()
    {
        if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
