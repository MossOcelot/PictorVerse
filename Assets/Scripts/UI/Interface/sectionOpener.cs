using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sectionOpener : MonoBehaviour
{
    public GameObject killSection;
    public GameObject Panel;
    public GameObject button;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
   // public GameObject backButton;
    //bool isPanelOn = false;


    public void OpenPanel()
    {
        //isPanelOn = false;
        killSection.SetActive(false);  
        Panel.SetActive(true);
        //Debug.Log("backbttn");
        //backButton.SetActive(true);
        //Debug.Log("backBttn2");
        button.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        
        Animator animator = Panel.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
        
        
    }

    public void backing()
    {  
        //backButton.SetActive(false);
        Animator animator = Panel.GetComponent<Animator>();
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");
            Debug.Log(isOpen);
            animator.SetBool("open", !isOpen);
        }
        button.SetActive(true);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Debug.Log($"Animation over");
            Panel.SetActive(false);
        }
        killSection.SetActive(true);
    }
}
