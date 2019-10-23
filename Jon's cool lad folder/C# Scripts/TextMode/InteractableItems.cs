using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItems : MonoBehaviour
{
    //List of all items that use the input action 'use' or 'press'
    public List<InteractableObject> usableItemList;

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    List<string> nounsInInventory = new List<string>();
    List<InteractableObject> itemsInInventory = new List<InteractableObject>();

    //List of action responses in the room
    public Dictionary<string, ActionResponse> responseDictionary = new Dictionary<string, ActionResponse>();

    GameController controller;
    TextInput textInput;
    UIFader uiFader;

    public float hackingVolume = 1f;

    void Awake()
    {
        controller = GetComponent<GameController>();
        textInput = GetComponent<TextInput>();
        uiFader = GetComponent<UIFader>();
    }

    void Start()
    {
        hackingVolume = controller.roomNavigation.sceneboi.musicVolume;
    }
    public bool TestForInteractibility(InteractableObject o)
    {
        //If an object is interactable 
        if (o.isInteractable)
        {
            return true;
        }

        else
        {
            controller.LogStringWithReturn(o.noInteractionString);
            return false;
        }
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        //Does what it says on the tin. If you have the item in your inventory, the item isn't displayed in the room
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        if (interactableInRoom == null)
        {
            Debug.LogError("This room has a null interacable object in it. Assign an object or set the list length to 0. Fucking idiot.");
        }

        if (!interactableInRoom.visibilityBeenChanged)
            interactableInRoom.isVisible = interactableInRoom.defaultVisibility;

        if (!nounsInInventory.Contains(interactableInRoom.noun) && interactableInRoom.isVisible)
        {
            nounsInRoom.Add(interactableInRoom.noun);              //noun = command keyword
            return interactableInRoom.description;
        }
        return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        //Collects all the items in the room and finds the interactable object it belongs to
        //For every object in the current room
        foreach (InteractableObject i in controller.roomNavigation.currentRoom.interactableObjectsInRoom)
        {
            if (i.interactions == null)
                continue;

            for (int j = 0; j < i.interactions.Length; j++)
            {
                //Gets any interactions the object may have
                Interaction interaction = i.interactions[j];
                if (interaction.actionResponse == null)
                    continue;
                //Adds this interaction to the dictionary if it isn't already in
                string keyString = i.name + j.ToString();
                if (!responseDictionary.ContainsKey(keyString))
                    responseDictionary.Add(keyString, interaction.actionResponse);
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    #region Input Actions
    public void DisplayInventory()                  //Checking what item you have
    {
        if (nounsInInventory.Count != 0)
        {
            controller.LogStringWithReturn("You are carrying: \n");

            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        }

        else
        {
            controller.LogStringWithReturn("You aren't carrying anything on you.");
        }
    }

    public Dictionary<string, string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            if (controller.roomNavigation.currentRoom.interactableObjectsInRoom.Length == 0)
            {
                controller.LogStringWithReturn("You don't see anything worth taking around here.");
                return null;
            }

            else
            {
                foreach (InteractableObject o in controller.roomNavigation.currentRoom.interactableObjectsInRoom)
                {
                    bool b = TestForInteractibility(o);
                    if (b)
                    {
                        nounsInInventory.Add(o.noun);
                        AddActionResponsesToUseDictionary();
                        nounsInRoom.Remove(o.noun);

                        itemsInInventory.Add(o);
                    }
                }
                return takeDictionary;
            }
        }


        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");            //if there is nothing to take then it respond with this.
            return null;
        }
    }
    #region Hacking

    public void HackItem(string[] separatedInputWords)
    {
        string nounToHack = separatedInputWords[1];
        if (nounsInRoom.Contains(nounToHack))
        {
            foreach (InteractableObject o in controller.roomNavigation.currentRoom.interactableObjectsInRoom)
            {
                if (o.noun == nounToHack)
                {
                    bool b = TestForInteractibility(o);
                    if (b)
                    {
                        //Start the hacking music
                        controller.roomNavigation.defaultTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        HackingController.hackingTrack.start();
                        HackingController.hackingTrack.setVolume(hackingVolume * controller.roomNavigation.masterVolume);
                        HackingController.hackingIntensity.setValue(1);

                        //Reset the important file found tracking bool
                        HackingController.importantFileFound = false ;

                        Debug.Log("Hacking");
                        controller.hacking = true;

                        //Find the correct library of files to load up for the player to interact with
                        HackingController.LoadLibrary(nounToHack);

                        //Switch UIs over
                        uiFader.FadeInPanel(controller.hackingUI);

                        //Switch the current input fields around, then run ActivateFields
                        textInput.currentField = textInput.hackingInputField;
                        textInput.notCurrentField = textInput.inputField;
                        controller.ClearCurrentLog();

                        ActivateFields();

                        //Switch the display output to begin logging & outputting the appropriate display
                        controller.currentActionLog = controller.hackingActionLog;
                        controller.currentDisplayText = controller.hackingDisplayText;
                    }
                }
            }
        }

        else
        {
            controller.LogStringWithReturn("You don't see a " + nounToHack + " here.");
        }
    }

    public void QuitHacking()
    {
        //UpdateFmod to appropriate intense levels
        if (HackingController.importantFileFound)
        {
            HackingController.hackingIntensity.setValue(7);
            GameController.RunFunctionAfterDelay(200, ResetHackingTrack);
        }

        else
        {
            HackingController.hackingIntensity.setValue(1);
            HackingController.hackingTrack.setVolume(0);
            HackingController.hackingTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            controller.roomNavigation.defaultTrack.start();
        }

        Debug.Log("Hacking over");
        controller.hacking = false;

        //Switch UI's over
        uiFader.FadeOutPanel(controller.hackingUI);

        //Switch the current input fields around, then run ActivateFields
        textInput.currentField = textInput.inputField;
        textInput.notCurrentField = textInput.hackingInputField;

        ActivateFields();

        //clear the hacking log so it becomes clear next time hacking is initiated

        controller.hackingActionLog.Clear();
        controller.hackingDisplayText.text = null;

        //Switch the display output to begin logging/outputting to the non hacking display once again
        controller.currentActionLog = controller.textModeActionLog;
        controller.currentDisplayText = controller.defaultDisplayText;

        controller.LogStringWithReturn("You key your Quikterm(tm) off and stash it back in your coat.");
    }

    public void ResetHackingTrack()
    {
        HackingController.hackingIntensity.setValue(1);
        HackingController.hackingTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        controller.roomNavigation.defaultTrack.start();
        controller.roomNavigation.defaultTrack.setVolume(controller.roomNavigation.musicVolume * controller.roomNavigation.masterVolume);
    }
    #endregion

    public void UseItem(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];

        if (nounsInInventory.Contains(noun))
        {
            bool useSuccess = false;

            foreach (InteractableObject o in itemsInInventory)
            {
                for (int i = 0; i < o.interactions.Length; i++)
                {
                    if (responseDictionary.ContainsKey(o.name + i.ToString()))
                    {
                        bool actionResult = responseDictionary[o.name + i.ToString()].DoActionResponse(controller);
                        if (!actionResult)
                        {
                            controller.LogStringWithReturn("You don't think you can use this " + noun + " like that here.");
                        }
                        else
                        {
                            useSuccess = true;
                            controller.LogStringWithReturn(o.interactions[i].textResponse);
                        }
                    }
                }
            }

            if (!useSuccess)
            {
                controller.LogStringWithReturn("You can't use the " + noun);
                Debug.LogWarning("If this item is meant to work, make sure it's added to the 'usableitems' list in GameController");
            }
        }

        else
        {
            controller.LogStringWithReturn("There is no " + noun + " in your inventory to use");
        }
    }

    public void Press(string[] separatedInputWords)
    {
        //If the player inputs anything wrong, this should guide them to using the command correctly.
        if ((separatedInputWords.Length != 4))
            controller.LogStringWithReturn("What are you trying to enter into what? (Press [code] on [object])");
        else
        {
            string code = separatedInputWords[1];
            string noun = separatedInputWords[2];

            if (separatedInputWords[2] != null)
            {
                if (separatedInputWords[2] == "on" || separatedInputWords[2] == "in")
                {
                    noun = separatedInputWords[3];
                }
            }
            bool codeFound = false;
            bool objectFound = false;

            foreach (InteractableObject o in controller.roomNavigation.currentRoom.interactableObjectsInRoom)
            {
                if (o.noun == noun)
                {
                    objectFound = true;
                    bool b = TestForInteractibility(o);
                    if (b)
                    {
                        for (int j = 0; j < o.interactions.Length; j++)
                        {
                            if (o.interactions[j].passCode != "")
                            {
                                if (o.interactions[j].passCode == code)
                                {
                                    codeFound = true;

                                    string keyString = o.name + j.ToString();
                                    Debug.Log("Attempting to use keystring" + keyString);
                                    bool actionResult = responseDictionary[keyString].DoActionResponse(controller);
                                    if (!actionResult)
                                    {
                                        controller.LogStringWithReturn("You don't think you can use this " + noun + " like that here.");
                                    }
                                }
                            }
                            else
                                controller.LogStringWithReturn("You cannot enter any kind of password onto the " + o.noun);
                        }
                    }
                }

                if (!objectFound)
                    controller.LogStringWithReturn("You don't see any " + noun + " in the room.");
                else if (!codeFound)
                    controller.LogStringWithReturn("The " + noun + " beeps angrily and displays 'Incorrect code'.");
            }
        }
    }

    public void Talk(string[] separatedInputWords)
    {
        string nounToTalk = separatedInputWords[1];

        if (separatedInputWords[1] != null)
        {
            if (separatedInputWords[1] == "to" || separatedInputWords[1] == "at")
            {
                nounToTalk = separatedInputWords[2];
            }
        }

        bool talkFound = false;

        if (nounsInRoom.Contains(nounToTalk))
        {
            foreach (InteractableObject o in controller.roomNavigation.currentRoom.interactableObjectsInRoom)
            {
                if (o.noun == nounToTalk)
                {
                    bool b = TestForInteractibility(o);
                    if (b)
                    {
                        talkFound = true;
                        Debug.Log("Talking");
                        controller.talking = true;

                        DialogueManager.LoadConversation(nounToTalk);
                    }
                }
            }

            if (talkFound == false)
            {
                controller.LogStringWithReturn("You don't think you can talk to the " + nounToTalk + ".");
            }
        }
    }
    #endregion


    public void ClearCollections()          //Clear the list of objects in the room
    {                                           
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }
    public void ActivateFields()
    {
        //Select Current field
        textInput.currentField.interactable = true;
        textInput.currentField.ActivateInputField();
        textInput.currentField.onEndEdit.AddListener(textInput.AcceptStringInput);
        //Deselect notCurrentField
        textInput.notCurrentField.interactable = false;
        textInput.notCurrentField.onEndEdit.RemoveAllListeners();
    }

    public void ReactivateTextInput()
    {
        textInput.currentField.ActivateInputField();
        textInput.currentField.onEndEdit.AddListener(textInput.AcceptStringInput);
    }
}