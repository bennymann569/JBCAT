using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/HackingActions/QuitHacking")]
public class QuitHacking : HackingAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.ClearCollectionsForNewRoom();
        controller.UnpackRoom();
        controller.interactableItems.QuitHacking();
    }
}