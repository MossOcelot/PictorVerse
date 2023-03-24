using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeManager : MonoBehaviour
{
    public bool reset = false;
    public bool IsOpen = false;

    float time;
    public void SetReset(bool reset)
    {
        this.reset = reset;
    }

    private void Update()
    {
        Debug.Log("Click");
        time += Time.deltaTime;
        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKey(KeyCode.Tab) && time > 0.5) 
        {
            Debug.Log("Clicks");
            bool active = gameObject.transform.GetChild(0).gameObject.activeInHierarchy;
            gameObject.transform.GetChild(0).gameObject.SetActive(!active);
            time = 0;
        }
    }
}
