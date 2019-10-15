using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/MakeRoomVisible")]
public class MakeRoomVisibleResponse : ActionResponse
{
    public Room roomToMakeVisible;

    public override bool DoActionResponse(GameController controller)
    {
        if (!roomToMakeVisible.isVisible)
        {
            Room.makeRoomVisible(roomToMakeVisible);
            return true;
        }

        else
        {
            Debug.Log("Room is already visible");
            return false;
        }
    }
}
