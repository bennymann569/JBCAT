using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/HackingActions/HackDisplay")]

public class HackDisplay : HackingAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length == 1)
        {
            controller.currentActionLog.Add("Please specify a file to display");
        }
        else
        {
            string stringToSearch = separatedInputWords[1];

            if (stringToSearch == "")
            {
                controller.currentActionLog.Add("Please specify a file to display");
            }
            else
            {
                HackingController.DisplayFile(stringToSearch);
            }
        }
    }
}