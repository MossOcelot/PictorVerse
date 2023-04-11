using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFileTax : MonoBehaviour
{
    public GameObject UI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (UI.activeSelf)
            {
                Close();
            } else
            {
                Open();
            }
        }
    }

    public void Open()
    {
        UI.SetActive(true);
    }

    public void Close()
    {
        UI.SetActive(false);
    }
}
