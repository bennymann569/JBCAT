using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotepadPanel : MonoBehaviour
{
    public bool notepadIsOpen = false;
    public KeyCode activationKey;
    public GameObject notepad;
    public TMP_InputField notepadTextField;



    public TextInput textInput;

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            if (!notepadIsOpen)
            {
                OpenNotepad();
                notepadIsOpen = true;
				//StartCoroutine (FadeTextToFullAlpha (1f, GetComponent<Text> ()));
            }

            else
            {
                CloseNotepad();
                notepadIsOpen = false;
				//StartCoroutine (FadeTextToZeroAlpha (1f, GetComponent<Text> ()));
            }
        }
    }

    public void NotepadButton()
    {
        if (notepadIsOpen == true)
        {
            CloseNotepad();
        }
        if (notepadIsOpen == false)
        {
            OpenNotepad();
        }
        notepadIsOpen = !notepadIsOpen;
    }

    public void OpenNotepad()
    {
        //Make main panel uninteractable
        textInput.currentField.interactable = false;

        //Make notebook active, then make it interactable, select it and move cursor to the end so everything isn't selectedt
        notepad.SetActive(true);
        notepadTextField.interactable = true;
        notepadTextField.ActivateInputField();
        notepadTextField.MoveTextEnd(true);
    }
    public void CloseNotepad()
    {
        //Make notepad uninteractable and remove the UI object
        notepad.SetActive(false);
        notepadTextField.interactable = false;

        //Make main panel active, then make it interactable, select it and move cursor to the end so everything isn't selected
        textInput.currentField.interactable = true;
        textInput.currentField.ActivateInputField();
        textInput.currentField.MoveTextEnd(true);
    }
}