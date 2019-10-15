using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeAudioIntensity")]
public class ChangeAudioIntensityResponse : ActionResponse
{
    public float audioIntensity;

    public override bool DoActionResponse(GameController controller)
    {
        if (controller.hacking)
            controller.roomNavigation.defaultTrack.start();

        controller.roomNavigation.intensity.setValue(audioIntensity);

        if (controller.hacking)
            controller.roomNavigation.defaultTrack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        //We don't want the intensity to go back down once we hit the peak at the end of the game
        if (audioIntensity == 5.7f)
            controller.roomNavigation.gameIsEnding = true;
        return true;
    }
}
