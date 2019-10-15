using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/MakeInteractable")]
public class MakeItemInteractableResponse : ActionResponse
{
    public InteractableObject targetObject;

    public override bool DoActionResponse(GameController controller)
    {
        if (targetObject != null)
        {
            targetObject.isInteractable = true;
            targetObject.interactabilityBeenChanged = true;
            return true;
        }

        else
        {
            Debug.LogError("No item assigned to be interactable as a part of this action response");
            return false;
        }
    }
}
