using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProfiles : MonoBehaviour
{

    public GameObject profile1;
    public GameObject profile2;

    public void ShowProfile()
    {
        profile1.SetActive(true);
        profile2.SetActive(true);

    }

    public void unShowProfile()
    {
        profile1.SetActive(false);
        profile2.SetActive(false);

    }
}
