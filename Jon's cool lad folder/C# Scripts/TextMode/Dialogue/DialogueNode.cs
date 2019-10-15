using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "TextAdventure/Dialogue node")]

public class DialogueNode : ScriptableObject 
{
    [TextArea]
	public string content;
	public DialogueLink[] links;
    [Space]
    public bool isStartNodeAndUsed = false;
    [Space]
    public ActionResponse[] actionResponses;
}
