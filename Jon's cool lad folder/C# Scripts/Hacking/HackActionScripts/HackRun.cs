using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/HackingActions/HackRun")]

public class HackRun : HackingAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length == 1)
        {
            controller.currentActionLog.Add("Please specify a file to run");
        }
        else
        {
            string stringToRun = separatedInputWords[1];

            if (stringToRun == "")
            {
                controller.currentActionLog.Add("Please specify a file to run");
            }
            else
            {
                HackingController.RunFile(stringToRun);
            }
        }
    }
}