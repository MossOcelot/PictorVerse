using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestDialogue", menuName = "Mission/QuestDialogue")]
public class QuestDialogue : ScriptableObject
{
    public string greeting;
    [TextArea]
    public List<string> conversation;
    [SerializeField] 
    private bool IsSayed;
    
    public void SetSayed(bool isSayed)
    {
        IsSayed = isSayed;
    }


}
