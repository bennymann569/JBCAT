using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
    static GameController controller;
    static RoomNavigation roomNavigation;

    public static DialogueNode currentNode;

	public void Awake()
	{
        controller = GetComponent<GameController>();
        roomNavigation = GetComponent<RoomNavigation>();
	}
    public static void LoadConversation(string inputString)
    {
        //For every object in the current room
        foreach (InteractableObject i in roomNavigation.currentRoom.interactableObjectsInRoom)
        {
            //If it has the same noun as the input
            if (i.noun == inputString)
            {
                if (i.dialogueFirstNode != null)
                {
                    if (i.dialogueFirstNode.isStartNodeAndUsed == false && i.returnDialogueNode != null)
                    {
                        DisplayDecisionNode(i.dialogueFirstNode);
                        i.dialogueFirstNode.isStartNodeAndUsed = true;
                    }

                    else if (i.returnDialogueNode == null)
                        DisplayDecisionNode(i.dialogueFirstNode);
                    else
                    {
                        DisplayDecisionNode(i.returnDialogueNode);
                    }
                }
                else
                {
                    Debug.LogError("Warning, No dialogue assigned to this NPC, you are about to fuck everything!");
                }
            }
        }
    }

	public static void DisplayDecisionNode(DialogueNode node)
	{
        currentNode = node;

        if (node.actionResponses != null)
            for (int i = 0; i < node.actionResponses.Length; i++)
            node.actionResponses[i].DoActionResponse(controller);

        //Write the contents of the node
        controller.LogStringWithReturn(node.content);

        if (node.links.Length == 0)
            controller.talking = false;

        //followed by the exits, assigning them numbers for future input
        for (int i = 0; i < node.links.Length; i++)
        { 
            controller.LogStringWithoutReturn("[" + (i+1) + "] " + node.links[i].description);
        }

        controller.LogStringWithReturn("");
    }
}