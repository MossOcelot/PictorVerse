using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestListPlayer", menuName = "Mission/QuestListPlayer")]
public class QuestListPlayer : ScriptableObject
{
    public List<Quest> QuestList;
}
