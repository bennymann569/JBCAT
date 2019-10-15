using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextAdventure/Interactable Object")]
public class InteractableObject : ScriptableObject

{
    public string noun = "name";
    [Header("Description")]
    [TextArea]
    public string description = "Description in room";
    [Header("Visibility")]
    public bool defaultVisibility = true;
    public bool isVisible;
    public bool visibilityBeenChanged = false;
    [Header("Interactions")]
    public bool defaultInteractability = true;
    public bool isInteractable;
    public bool interactabilityBeenChanged = false;
    [TextArea]
    public string noInteractionString;
    public Interaction[] interactions;
    [Header("Terminals")]
    public FileLibrary fileLibrary;
    [Header("NPCs")]
    public DialogueNode dialogueFirstNode;
    public DialogueNode returnDialogueNode;
}
