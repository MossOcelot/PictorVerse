using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using JetBrains.Annotations;

public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public QuestDialogue DefaultDialogue;
    public GameObject TimeLine;
    public GameObject Sign;
    public List<string> dialogue;
    public NPC_Quest npc_quest;
    public NPC_Reach npc_reach;
    public int index = 0;
    public Camera mainCamera;
    
    public float wordSpeed;
    public bool playerIsClose;
    public bool IsEndSituation = false;
    public bool IsOpenShelf = false;
    public bool IsInQuest;
    public bool IsInReachQuest;
    public bool isPuzzle = false;
    public bool isPuzzling = false;


    public void Start()
    {
        isPuzzling = false;
    }

    void Update()
    {

       
        if (!playerIsClose) return;
        if (IsInQuest)
        {
            int len = dialoguePanel.transform.GetChild(3).gameObject.transform.childCount;
            for (int i = 0; i < len; i++)
            {
                dialoguePanel.transform.GetChild(3).gameObject.transform.GetChild(i).gameObject.SetActive(false);
                
            }
        }
        else
        {
            dialoguePanel.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (!dialoguePanel.active)
        {
            IsEndSituation = false;
            RemoveText();
        }
        if (Input.GetKeyDown(KeyCode.E) && !IsOpenShelf && isPuzzling ==false)
        {

            if (isPuzzling)
            {
                StartDialogue();
                player.gameObject.GetComponent<PlayerMovement>().isLooking = true;

            }
            if(isPuzzling == false)
            {

                if (!dialoguePanel.activeInHierarchy)
                {
                    if (!IsInQuest)
                    {
                        dialogue.Clear();
                        dialogue.Add(DefaultDialogue.greeting);
                        dialogue.AddRange(DefaultDialogue.conversation);
                    }
                    if (npc_reach != null)
                    {
                        QuestDialogue reachQuestDialogue = npc_reach.CheckQuestInPlayer();
                        if (reachQuestDialogue != null)
                        {
                            IsInReachQuest = true;
                            dialogue.Clear();
                            dialogue.Add(reachQuestDialogue.greeting);
                            dialogue.AddRange(reachQuestDialogue.conversation);
                        }
                    }
                    StartDialogue();
                }
                else if (dialogueText.text == dialogue[index] || IsInQuest)
                {
                    NextLine();
                }
            }



        }
        
        if(IsInQuest && IsEndSituation && TimeLine != null)
        {
            TimeLine.SetActive(true);
            if (mainCamera != null)
            {
                isPuzzle = true;
                mainCamera.orthographicSize = 18f;

            }

        }
        else if(IsEndSituation && IsInReachQuest && TimeLine != null)
        {
            TimeLine.SetActive(true);
            if (mainCamera != null)
            {
                isPuzzle = true;
                mainCamera.orthographicSize = 18f;

            }
        }

        if (IsInQuest && IsEndSituation && Sign != null)
        {
            Sign.SetActive(false);
        }
        /*if (Input.GetKeyDown(KeyCode.q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }*/
    }
    public void StartDialogue()
    {if(isPuzzle == false)
        {
dialoguePanel.SetActive(true);

        if (index == dialogue.Count - 1)
        {
            IsEndSituation = true;
        }
        if (player != null)
        {
            player.gameObject.GetComponent<PlayerMovement>().isLooking = true;
        }
        StartCoroutine(Typing());
        }if(isPuzzle == true)
        {
            TimeLine.SetActive(true);
            player.gameObject.GetComponent<PlayerMovement>().isLooking = true;

        }

    }
    public void SetIndexConversation(int index)
    {
        this.index = index;
    }

    
    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        int len = dialoguePanel.transform.GetChild(3).gameObject.transform.childCount;
        Debug.Log("Len Button: " + len);
        for (int i = 0; i < len - 1; i++)
        {
            dialoguePanel.transform.GetChild(3).gameObject.transform.GetChild(i + 1).gameObject.SetActive(false);
        }
        if (player != null)
        {
            player.gameObject.GetComponent<PlayerMovement>().isLooking = false;
        }
        dialoguePanel.SetActive(false);

        if (IsInQuest)
        {
            npc_quest.SendQuest();
            IsInQuest = false;
        }

        if (IsInReachQuest)
        {
            npc_reach.FinishReach();
            IsInReachQuest = false;
        }
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
        if (index < dialogue.Count - 1)
        {
            index++;
            if (index == dialogue.Count - 1)
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

    public void SetIsOpenShelft(bool IsOpen)
    {
        IsOpenShelf = IsOpen;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            player = GameObject.FindGameObjectWithTag("Player");
            dialoguePanel = GameObject.FindGameObjectWithTag("Dialog").gameObject.transform.GetChild(0).gameObject;
            dialogueText = dialoguePanel.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            dialogueText.text = "";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsOpenShelf = false;
            IsEndSituation = false;
            playerIsClose = false;
            RemoveText();
            player = null;
            TimeLine.SetActive(false);

        }
    }
}