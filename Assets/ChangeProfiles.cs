using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProfiles : MonoBehaviour
{

    public GameObject profile1;
    public GameObject statusprofile1;


    public void ShowProfile()
    {
        profile1.SetActive(true);
        statusprofile1.SetActive(true);
    }

    public void unShowProfile()
    {
        profile1.SetActive(false);
        statusprofile1.SetActive(false);

    }
}
