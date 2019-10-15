using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse
{
    public Room roomToChangeTo;

    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom == roomToBeIn)
        {
            controller.roomNavigation.currentRoom = roomToChangeTo;
            controller.roomNavigation.ChangeRooms();
            return true;
        }
        return false;
    }
}
