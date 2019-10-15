using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    public TMP_InputField currentField, notCurrentField, inputField, hackingInputField;
    public ScrollRect scrollRect;

    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
        currentField = inputField;
        currentField.onEndEdit.AddListener(AcceptStringInput);
    }

    void Start()
    {
        inputField.ActivateInputField();
    }

    public void AcceptStringInput(string userInput)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            return;
        }
        else if (!PauseMenu.gameIsPaused)
        {
            if (!AnimatedDialogueText.textIsAnimating)
            {
                if (userInput != "")
                {
                    userInput = userInput.ToLower();

                    char[] delimiterCharacters = { ' ' };
                    string[] separatedInputWords = userInput.Split(delimiterCharacters);

                    //If hacking is happening, use the hacking list of commands
                    if (controller.hacking)
                    {
                        for (int i = 0; i < controller.hackingActions.Length; i++)
                        {
                            HackingAction hackingAction = controller.hackingActions[i];
                            if (hackingAction.keyWord == separatedInputWords[0])
                            {
                                hackingAction.RespondToInput(controller, separatedInputWords);
                            }
                        }
                    }
                    //Otherwise, if talking is happening, use the talkign list of commands
                    else if (controller.talking)
                    {
                        for (int i = 0; i < DialogueManager.currentNode.links.Length; i++)
                        {
                            if (int.Parse(userInput)-1 == i)
                            {
                                if (DialogueManager.currentNode.links[i] != null)
                                {
                                    controller.currentActionLog.Add(userInput);
                                    DialogueManager.DisplayDecisionNode(DialogueManager.currentNode.links[i].destinationNode);
                                }
                            }
                        }
                    }

                    //If neither are happening, use the InputActions list of commands
                    else
                    {
                        controller.LogStringWithReturn(userInput);
                        for (int i = 0; i < controller.inputActions.Length; i++)
                        {
                            InputAction inputAction = controller.inputActions[i];
                            if (inputAction.keyWords.Contains(separatedInputWords[0]))
                            {
                                inputAction.RespondToInput(controller, separatedInputWords);
                            }
                        }
                    }

                    InputComplete();
                }
            }
            else
            {
                InputComplete();
            }
        }
    }

    public void InputComplete()
    {
        //Now that the output has been calculated, add the next text to the logs, then clear the input field and reactivate it for the next command
        ForceScrollDown();
        controller.DisplayLoggedText();

        currentField.ActivateInputField();
        currentField.text = null;
    }

    public void ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}


