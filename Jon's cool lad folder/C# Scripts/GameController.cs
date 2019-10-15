using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    // Declare Output Textboxes and their logs

    public TextMeshProUGUI defaultDisplayText, hackingDisplayText, currentDisplayText;
    [HideInInspector]
    public List<string> currentActionLog, oldActionLog, textModeActionLog, hackingActionLog = new List<string>();

    //List all keywords the player can use as verbs
    public InputAction[] inputActions;
    public HackingAction[] hackingActions;

    //Reference other scripts in the GameController object
    [HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public InteractableItems interactableItems;
    [HideInInspector] public UIFader uiFader;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public AnimatedDialogueText textAnimator;    
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();

    //Statuses that can change our input commands or UI
    public bool hacking = false;
    public bool talking = false;

    //UI
    Canvas canvas;
    RectTransform canvasRekt;
    float canvasWidth;

    public GameObject defaultUI, hackingUI;

    TextInput textInput;

    private GameObject tipBox; 
    private RectTransform tipBoxRectTransform;
    public bool tipActiveGC = false;

    //Audio
    public AudioSource keySound;

    //Variables for the waiting Function
    static bool waiting = false;
    static int countingUpInt = 0;
    static int intToCountTo;
    static Action method;

    void Awake()
    {
        //Get a bunch of components attached to this game object
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
        uiFader = GetComponent<UIFader>();
        dialogueManager = GetComponent<DialogueManager>();
        textAnimator = GetComponent<AnimatedDialogueText>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasRekt = canvas.GetComponent<RectTransform>();
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width * canvas.GetComponent<RectTransform>().localScale.x;
        textInput = GetComponent<TextInput>();

        tipBox = GameObject.Find("Tipbox");
        tipBoxRectTransform = GameObject.Find("ToolTipText").GetComponent<RectTransform>();

        //Set default display to currently being used (as opposed to hacking)
        currentDisplayText = defaultDisplayText;
        currentActionLog = textModeActionLog;

        //Clear the the displays of their default text
        defaultDisplayText.text = " ";
        hackingDisplayText.text = " ";
    }

    void Start()
    {
        roomNavigation.ChangeRooms();
        DisplayLoggedText();
    }

    void Update()
    {
        if (hacking && !PauseMenu.gameIsPaused)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
                    return;
                else
                {
                    keySound.Play();
                }

            }
        }

        if (waiting)
        {
            if (countingUpInt < intToCountTo)
            {
                countingUpInt++;
            }
            else
            {
                waiting = false;
                countingUpInt = 0;
                method();
            }
        }

        //Bind the tooltip box to the mouse position (even if it is not visible)
        //Additionally, make it on the right hand side of the mouse if on the left of the screen, and vice versa

        if (tipActiveGC == true)
            tipBox.SetActive(true);

        else
            tipBox.SetActive(false);

        if (Input.mousePosition.x > canvasWidth / 2)
            tipBox.transform.position = Input.mousePosition;
        else
            tipBox.transform.position = new Vector2(Input.mousePosition.x + (tipBoxRectTransform.rect.width * canvasRekt.localScale.x), Input.mousePosition.y);
    }

    public void DisplayLoggedText()
    {
        //Outputs all logged text into the currentDisplayText
        string logAsText = string.Join("\n", currentActionLog.ToArray());

        if (!AnimatedDialogueText.textIsAnimating)
        {
            if (!hacking)
            {
                LogStringWithReturn("-----------------------------------------------------------------------------------------------------------------------");
            }
            logAsText = string.Join("\n", currentActionLog.ToArray());
            textAnimator.AnimateText(logAsText, currentDisplayText);
            oldActionLog.AddRange(currentActionLog);

            ClearCurrentLog();
        }

        else
        {
            textAnimator.CompleteAnimateText();
        }
    }

    public void LogStringWithoutReturn(string stringToAdd)
    {
        //Adds one line to the log, ready to output
        currentActionLog.Add(stringToAdd);
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        //Adds one line and a carriage return to the log, ready to output
        currentActionLog.Add(stringToAdd + "\n");
    }

    public void ClearCurrentLog()
    {
        //Wipes the entire log (used for loading the game from fresh)
        currentActionLog.Clear();
    }

    #region Text outputting
    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        UnpackRoom();

        OutputRoomDescription();
    }

    public void ClearCollectionsForNewRoom()
    {
        //Clears the various lists used for figuring out what's displayed in each room, so that it can be ready to accept new parameters

        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        //interactableItems.responseDictionary.Clear();
        roomNavigation.ClearExits();
    }

    public void UnpackRoom()
    {
        //Adds the room's exits and items to a list so they can be interacted with and seen
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToInteract(roomNavigation.currentRoom);
    }

    public void OutputRoomDescription()
    {
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    #endregion

    #region Items
    void PrepareObjectsToInteract(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            //Add the item descriptions to the display dictionary if they aren't in already
			string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory (currentRoom, i);
			if (descriptionNotInInventory != null) 
			{
				interactionDescriptionsInRoom.Add (descriptionNotInInventory);
			}

			InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if (interaction.inputAction.keyWords.Contains("examine"))
                {
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);     //Examine Item(Connect to Text response) (Script: Interaction) 
                }

                if (interaction.inputAction.keyWords.Contains("take"))
                {
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);     //take Item(Connect to Text response) (Script: Interaction)
                }
            }
        }

        interactableItems.AddActionResponsesToUseDictionary();
    }
    #endregion

    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)		//see if the noun are in the dictionary
	{
        if (verbDictionary.ContainsKey(noun))
        {
            foreach (InteractableObject o in roomNavigation.currentRoom.interactableObjectsInRoom)
            {
                if (o.noun == noun && o.isVisible)
                return verbDictionary[o.noun];            //if the noun is in the dictionary, return the string to whatever it called from
            }
        }

		return "You can't see a " + noun + " to " + noun;		//if not in the dictionary, it return "you cant".
	}

    public static void RunFunctionAfterDelay(int timeToWait, Action functionToRun)
    {
        method = functionToRun;
        intToCountTo = timeToWait;
        waiting = true;
    }

    public void ToggleUIPanel(GameObject g)
    {
        if (g.activeSelf == true)
        {
            uiFader.FadeOutPanel(g);

            if (g = GameObject.Find("Textbox"))
            {
                textInput.currentField.interactable = false;
                textInput.currentField.onEndEdit.RemoveAllListeners();
            }
        }

        else
        {
            uiFader.FadeInPanel(g);

            if (g = GameObject.Find("Textbox"))
            {
                textInput.currentField.interactable = true;
                textInput.currentField.ActivateInputField();
                textInput.currentField.onEndEdit.AddListener(textInput.AcceptStringInput);
            }
        }
    }
}