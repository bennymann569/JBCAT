using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/HackingActions/HackSearch")]

public class HackSearch : HackingAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length == 1)
        {
            controller.currentActionLog.Add("Please specify a file to search, or search 'all'");
        }
        else
        {
            string stringToSearch = separatedInputWords[1];

            if (stringToSearch == "")
            {
                controller.currentActionLog.Add("Please specify a file to search, or search 'all'");
            }
            else
            {
                HackingController.SearchFile(stringToSearch);
            }
        }
    }
}