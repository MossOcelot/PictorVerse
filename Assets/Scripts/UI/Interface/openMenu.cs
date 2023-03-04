using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openMenu : MonoBehaviour
{
    Button mainButton;
    bool isExpanded = false;
    public GameObject openItems;
    public GameObject closeItems = null;
    void Start()
    {
        openItems.SetActive(false);
        mainButton = transform.GetComponent<Button>();
        Debug.Log("run");
        mainButton.onClick.AddListener(ToggleMenu);
        Debug.Log("run2");
        // mainButton.transform.SetAsLastSibling();

    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;
    
        if (isExpanded)
        {
            openItems.SetActive(false);
            closeItems.SetActive(true);
        }
        else
        {
            openItems.SetActive(true);
            closeItems.SetActive(false);
        }
    }
    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);

    }
}
