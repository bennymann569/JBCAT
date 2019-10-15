using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/DeleteItem")]
public class DeleteItemResponse : ActionResponse
{
    public InteractableObject objectToDelete;

    public override bool DoActionResponse(GameController controller)
    {
        if (objectToDelete != null)
        {
            objectToDelete.isVisible = false;
            objectToDelete.visibilityBeenChanged = true;
            return true;
        }

        else
        {
            Debug.LogError("No item assigned to be deleted as a part of this action response");
            return false;
        }
    }
}
