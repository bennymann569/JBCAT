using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


[System.Serializable]

public class SaveLoadManager : MonoBehaviour
{
    public GameController controller;
    public RoomNavigation roomNavigation;
    public NotepadPanel notepadPanel;
    public SceneStorage sceneBoi;

    Hashtable gameDataHash = new Hashtable();
    Hashtable settingsDataHash = new Hashtable();


    private void Start()
    {
        //If you've loaded in from the main menu, do a normal load game function to get the player back to where they were.
        if (sceneBoi.loadedFromMainMenu)
        {
            LoadGame();
            sceneBoi.loadedFromMainMenu = false;
        }
    }
    
    public void SaveGame()
    {
        gameDataHash = new Hashtable();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        string roomData = roomNavigation.currentRoom.name.ToString();
        string notepadData = notepadPanel.notepadTextField.text;

        gameDataHash.Add("room", roomData);
        gameDataHash.Add("notepad", notepadData);

        try
        {
            bf.Serialize(stream, gameDataHash);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            stream.Close();
        }

        sceneBoi.aSaveExists = true;

    }

    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {

            FileStream fs = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            gameDataHash = null;

            // Open the file containing the data that we want to deserialize.

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                gameDataHash = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            //Set the notepad to what it was saved as
            string notePad = (string)gameDataHash["notepad"];
            notepadPanel.notepadTextField.text = notePad;

            string roomData = (string)gameDataHash["room"];
            roomNavigation.currentRoom = controller.roomNavigation.SearchForRoom(roomData);
            //if text is scrolling when the game is loaded, we want it to just complete so that the new room's description can be displayed.
            if (AnimatedDialogueText.textIsAnimating)
                controller.textAnimator.CompleteAnimateText();
            //teleport player to saved room, deleting the logs of whatever's previously been typed.
            controller.currentDisplayText.text = null;
            roomNavigation.ChangeRooms();
            controller.DisplayLoggedText();

        }
    }

    public void StoreData()
    {
        settingsDataHash = new Hashtable();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/settings.sav", FileMode.Create);

        string saveFileExistsData = controller.roomNavigation.sceneboi.aSaveExists.ToString();
        string roomData = roomNavigation.currentRoom.name.ToString();
        string notepadData = notepadPanel.notepadTextField.text;
        float masterVolumeData = controller.roomNavigation.sceneboi.masterVolume;
        float musicVolumeData = controller.roomNavigation.sceneboi.musicVolume;
        float keyVolumeData = controller.roomNavigation.sceneboi.keyVolume;
        float mainMenuVolumeData = controller.roomNavigation.sceneboi.mainMenuVolume;

        settingsDataHash.Add("saveFile", saveFileExistsData);
        settingsDataHash.Add("room", roomData);
        settingsDataHash.Add("notepad", notepadData);
        settingsDataHash.Add("masterVolume", masterVolumeData);
        settingsDataHash.Add("musicVolume", musicVolumeData);
        settingsDataHash.Add("keyVolume", keyVolumeData);
        settingsDataHash.Add("mainVolume", mainMenuVolumeData);

        try
        {
            bf.Serialize(stream, settingsDataHash);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            stream.Close();
        }
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/settings.sav"))
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            settingsDataHash = null;

            // Open the file containing the data that we want to deserialize.

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                settingsDataHash = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            //Begin setting all of sceneBoi's data to match the .sav file's records
            if ((string)settingsDataHash["saveFile"] == "true")
                sceneBoi.aSaveExists = true;
            else
                sceneBoi.aSaveExists = false;

            //Set the notepad to what it was saved as
            string notePad = (string)settingsDataHash["notepad"];
            notepadPanel.notepadTextField.text = notePad;

            string roomData = (string)settingsDataHash["room"];
            roomNavigation.currentRoom = controller.roomNavigation.SearchForRoom(roomData);


        }

    }
}