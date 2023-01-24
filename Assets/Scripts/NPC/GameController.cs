using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam,Dialog}

public class GameController : MonoBehaviour
{
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
            Debug.Log("Helloworld");

        };
    }

    // Update is called once per frame
    private void Update()
    {
        if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
