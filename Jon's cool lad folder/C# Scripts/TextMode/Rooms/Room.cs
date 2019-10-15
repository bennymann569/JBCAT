using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    [HideInInspector]
    public enum Track { light, medium, heavy };
    [TextArea]
    public string description;
    public Exit[] exits;
    [Header("Interactions")]
    public InteractableObject[] interactableObjectsInRoom;
    public Interaction[] interaction;
    [Header("Image")]
    public TooltipImage toolImage;
    [Header("Visibility")]
    public bool defaultVisibility = true;
    public bool isVisible;
    public Room[] roomsTriggeredToVisible;
    [Header("Audio")]
    public Track track;
    public float audioFloorIntensity = 0;
    public AudioClip musicClip;
    public AudioClip sfxClip;
    public AudioClip ambientClip;

    public static void makeRoomVisible(Room inputRoom)
    {
        if (!inputRoom.isVisible)
            inputRoom.isVisible = true;

        else
            Debug.Log(inputRoom.name.ToString() + " is already visible");
    }
    public override string ToString()
    {
        return "Room :" + this.name;
    }
}
