using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DropdownGalaxyMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject section1;
    [SerializeField] private GameObject section2;
    [SerializeField] private GameObject section3;
    [SerializeField] private GameObject section4;
    [SerializeField] private GameObject section5;

    [SerializeField] private GameObject killSection;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;
    [SerializeField] private GameObject button4;
    [SerializeField] private GameObject button5;
    //[SerializeField] private GameObject backButton;

    public void dropdownMenu(int index)
    {
        switch (index)
        {
            case 0:
                panel.SetActive(true);
                section1.SetActive(false);
                section2.SetActive(false);
                section3.SetActive(false);
                section4.SetActive(false);
                section5.SetActive(false);
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
                button5.SetActive(true);
                killSection.SetActive(true);
                break;
            case 1:
                section1.SetActive(true);
                section2.SetActive(false);
                section3.SetActive(false);
                section4.SetActive(false);
                section5.SetActive(false);

                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                button5.SetActive(false);
                killSection.SetActive(false);
                Animator animator = section1.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", !isOpen);
                }
                break;   
            case 2:
                section2.SetActive(true);
                section1.SetActive(false);
                section3.SetActive(false);
                section4.SetActive(false);
                section5.SetActive(false);

                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                button5.SetActive(false);
                killSection.SetActive(false);
                Debug.Log("active");
                animator = section2.GetComponent<Animator>();
                Debug.Log("aaaa");
                if (animator != null)
                {
                    Debug.Log("notnull");
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", !isOpen);
                }
                 break;
            case 3:
                section3.SetActive(true);
                section2.SetActive(false);
                section1.SetActive(false);
                section4.SetActive(false);
                section5.SetActive(false);

                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                button5.SetActive(false);
                killSection.SetActive(false);
                animator = section3.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", !isOpen);
                }
                 break;
            case 4:
                section4.SetActive(true);
                section2.SetActive(false);
                section3.SetActive(false);
                section1.SetActive(false);
                section5.SetActive(false);

                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                button5.SetActive(false);
                killSection.SetActive(false);
                animator = section4.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", !isOpen);
                }
                 break;
            case 5:
                section5.SetActive(true);
                section2.SetActive(false);
                section3.SetActive(false);
                section4.SetActive(false);
                section1.SetActive(false);

                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
                button5.SetActive(false);
                killSection.SetActive(false);
                animator = section5.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");
                    animator.SetBool("open", !isOpen);
                }
                 break;

        }
    }

}
