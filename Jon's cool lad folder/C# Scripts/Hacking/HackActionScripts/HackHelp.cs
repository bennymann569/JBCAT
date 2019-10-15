using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/HackingActions/HackHelp")]
public class HackHelp : HackingAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogStringWithReturn("Search - Use this to search for files on this terminal. Accepted inputs include 'all'" + "\n" + "Display - Use this to display a specific file. NB: You don't have to type the full file name out." + "\n" + "Run: Runs a .exe file, used to interact with hardware connected to this Quickterm." + "\n" + "Help - Displays this function." + "\n" + "Quit - Use this to log off your terminal securely. A safe grid is a happy grid!");
    }
}