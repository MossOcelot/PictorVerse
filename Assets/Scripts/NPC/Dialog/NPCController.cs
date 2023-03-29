using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;
    public bool IsEndSituation = false;

    // Update is called once per frame
    void Update()
    {
        if (!playerIsClose) return;
        if (!dialoguePanel.active)
        {
            IsEndSituation = false;
            RemoveText();
        }
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);

                if (index == dialogue.Length - 1)
                {
                    IsEndSituation = true;
                }
                if (player != null)
                {
                    player.gameObject.GetComponent<PlayerMovement>().isLooking = true;
                }
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }


        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        int len = dialoguePanel.transform.GetChild(3).gameObject.transform.childCount;
        for (int i = 0; i < len - 1; i++)
        {
            dialoguePanel.transform.GetChild(3).gameObject.transform.GetChild(i + 1).gameObject.SetActive(false);
        }
        if (player != null)
        {
            player.gameObject.GetComponent<PlayerMovement>().isLooking = false;
        }
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            if (index == dialogue.Length - 1)
            {
                IsEndSituation = true;
            }
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            player = GameObject.FindGameObjectWithTag("Player");
            //player = other.gameObject;
            dialoguePanel = GameObject.FindGameObjectWithTag("Dialog").gameObject.transform.GetChild(0).gameObject;
            dialogueText = dialoguePanel.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            dialogueText.text = "";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            IsEndSituation = false;
            playerIsClose = false;
            RemoveText();
            player = null;
        }
    }
}