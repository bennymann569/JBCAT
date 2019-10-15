using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RoomNavigation : MonoBehaviour
{
    GameController controller;

    public GameObject imagePanel;

    public SceneStorage sceneboi;

    public Object[] allRooms, allItems, allDialogues;
    public Room currentRoom;
    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    GameObject[] allTooltips;

    //Shit for Fmod audio
    public FMOD.Studio.EventInstance defaultTrack;
    public FMOD.Studio.EventInstance lightTrack;
    public FMOD.Studio.EventInstance mediumTrack;
    public FMOD.Studio.EventInstance heavyTrack;
    public FMOD.Studio.ParameterInstance intensity;

    public float masterVolume;
    public float musicVolume;
    public float keyVolume;
    string currentTrack;
    public bool gameIsEnding;

    void Awake()
    {
        controller = GetComponent<GameController>();

        //Create a reference to all the rooms, items, and starting dialogue in the game, and define their visibility
        allRooms = Resources.LoadAll("ScriptableObjects/Rooms", typeof(Room));
        Debug.Log("Rooms loaded: " + allRooms.Length);

        allItems = Resources.LoadAll("ScriptableObjects/Items", typeof(InteractableObject));
        Debug.Log("Items loaded: " + allItems.Length);

        allDialogues = Resources.LoadAll("ScriptableObjects/Dialogue", typeof(DialogueNode));
        Debug.Log("Dialogue nodes loaded: " + allDialogues.Length);

        //Setup FMOD audio
        gameIsEnding = false;
        lightTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Light_ambient");
        mediumTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Neutral_ambient");
        heavyTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Tense_ambient");
        defaultTrack.getParameter("Intensity", out intensity);
        defaultTrack = lightTrack;
        currentTrack = "light";

        masterVolume = sceneboi.masterVolume;
        musicVolume = sceneboi.musicVolume;
        keyVolume = sceneboi.keyVolume;


        //If rooms/items are invisible by default, set them to invisible. This is being called because scriptableobjects do not get reset upon scene loading, they are storage data, not gameobjects
        foreach (Room r in allRooms)
        {
            if (r.defaultVisibility)
                r.isVisible = true;
            else
                r.isVisible = false;
        }

        foreach (InteractableObject o in allItems)
        {
            o.visibilityBeenChanged = false;

            if (o.defaultVisibility)
                o.isVisible = true;
            else
                o.isVisible = false;
            if (o.defaultInteractability)
                o.isInteractable = true;
            else
                o.isInteractable = false;
        }

        foreach (DialogueNode d in allDialogues)
            d.isStartNodeAndUsed = false;

        //Compile a list of all tool tips. We'll reference these with their keys later to match them to images
        allTooltips = GameObject.FindGameObjectsWithTag("Tooltip");
        ClearTipTriggers();

        LoadImage();
    }

    void Start()
    {
        defaultTrack.start();
        defaultTrack.setVolume(musicVolume * masterVolume);
    }

    public void UnpackExitsInRoom()
    {
        if (currentRoom != null && currentRoom.exits != null)
        {
            foreach (Exit e in currentRoom.exits)
            {
                //Add room to the list of exits and print its description ONLY if it is visible.
                exitDictionary.Add(e.keyString, e.valueRoom);
                if (e.valueRoom != null)
                {
                    if (e.valueRoom.isVisible)
                        controller.interactionDescriptionsInRoom.Add(e.exitDescription);
                }

                else Debug.LogError("This room has a null exit in it, please fix");
            }
        }
        else
            Debug.Log("No exits in this room!");
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        //For now, invisible rooms can still be travelled to, if we no longer want that, we just need to add in a clause to exclude invisible rooms in the if statement below
        if (exitDictionary.ContainsKey(directionNoun))
        {
            //Change currentRoom into the room correlating to exit noun.
            currentRoom = exitDictionary[directionNoun];
            ChangeRooms();
        }
        else
        {
            controller.LogStringWithReturn("You can't seem to go that way.");
        }
    }

    public void ChangeRooms()
    {
        //We're about to change rooms, so it's important we remove the current tooltip trigger boxes so we can add the new ones without them stacking infinitely
        ClearTipTriggers();

        controller.DisplayRoomText();

        //Change audio track/intensity if applicble
        ChangeAudio();

        Debug.Log("Room changed to: " + currentRoom.name);

        //Trigger rooms to be made visible, if applicable
        if (currentRoom.roomsTriggeredToVisible != null)
        {
            foreach (Room r in currentRoom.roomsTriggeredToVisible)
            {
                Room.makeRoomVisible(r);
            }
        }

        LoadImage();
    }

    public void ChangeAudio()
    {
        if (currentRoom.track == Room.Track.light)
        {
            if (currentTrack != currentRoom.track.ToString())
            {
                defaultTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                defaultTrack = lightTrack;
                defaultTrack.start();
            }

        }

        else if (currentRoom.track == Room.Track.medium)
        {
            if (currentTrack != currentRoom.track.ToString())
            {
                defaultTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                defaultTrack = mediumTrack;
                currentTrack = "medium";
                defaultTrack.start();
            }
            mediumTrack.getParameter("Intensity", out intensity);
        }

        else if (currentRoom.track == Room.Track.heavy)
        {
            if (currentTrack != currentRoom.track.ToString())
            {
                defaultTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                defaultTrack = heavyTrack;
                currentTrack = "heavy";
                defaultTrack.start();
            }
            heavyTrack.getParameter("Intensity", out intensity);
        }

        else
            Debug.Log("You've somehow made it to a room with none of the three enums above. Is your room even set up correclt?");

        //Now the correct track is chosen, start it with the volume levels the player has chosen
        controller.roomNavigation.defaultTrack.setVolume(masterVolume * musicVolume);

        //Assuming the game isn't ending and we're not in the final room of the game, change the audio levels appropriately
        if ((!gameIsEnding && currentRoom.audioFloorIntensity != 0) || (gameIsEnding && controller.roomNavigation.currentRoom.name == "Level complete"))
            intensity.setValue(currentRoom.audioFloorIntensity);
    }

    public void ClearTipTriggers()
    {
        foreach (GameObject g in allTooltips)
        {
            controller.tipActiveGC = false;
            g.GetComponent<TipTrigger>().enabled = false;
            g.GetComponent<Image>().enabled = false;
        }
    }

    public void LoadImage()
    {
        if (currentRoom.toolImage != null)
        {
            //If the room has a new image to display, switch the image over
            imagePanel.GetComponent<Image>().sprite = currentRoom.toolImage.image;

            //If the new room has any tooltips to add, set them to be active
            if (currentRoom.toolImage.imageID != null)
            {
                foreach (GameObject g in allTooltips)
                {
                    if (g.GetComponent<TipTrigger>().imageID == currentRoom.toolImage.imageID)
                    {
                        g.GetComponent<TipTrigger>().enabled = true;
                        g.GetComponent<Image>().enabled = true;
                    }
                }
            }


        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }

    public Room SearchForRoom(string data)
    {
        bool foundRoom = false;
        foreach (Room r in allRooms)
        {
            if (r.name.ToString() == data)
            {
                foundRoom = true;
                Debug.Log("Searching for room found: " + r);
                return r;
            }
        }
        if (!foundRoom)
        {
            Debug.Log("A room has been saved that isn't currently in the scene or has an incorrect name");
            return null;
        }
        else
            return null;
    }
}
