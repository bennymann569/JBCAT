using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class HackingController : MonoBehaviour
{
    public static GameController controller;
    public static RoomNavigation roomNavigation;

    public static FileLibrary currentLoadedFiles;

    public static List<HackableFile> foundFiles = new List<HackableFile>();

    public static Object[] allTerminals;

    public static bool importantFileFound = false;
    static string tempString;

    //Shit for Fmod audio
    public static FMOD.Studio.EventInstance hackingTrack;
    public static FMOD.Studio.ParameterInstance hackingIntensity;

    void Awake()
    {
        controller = GetComponent<GameController>();
        roomNavigation = GetComponent<RoomNavigation>();

        hackingTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Hacking_theme");
        hackingTrack.getParameter("Hacking_progress", out hackingIntensity);
        hackingTrack.start();
        hackingTrack.setVolume(0);
    }

    public static void LoadLibrary(string inputString)
    {
        //For every object in the current room
        foreach (InteractableObject i in roomNavigation.currentRoom.interactableObjectsInRoom)
        {
            //If it has the same noun as the input
            if (i.noun == inputString)
            {
                if (i.fileLibrary == null)
                {
                    Debug.LogError("Warning, No FileLibrary assigned to this Terminal, you are about to fuck everything!");
                }
                else
                {
                    currentLoadedFiles = i.fileLibrary;
                }
            }
        }
    }

    static void SearchLibrary(string inputString)
    {
        //Populate a list of files that match inputString
        foreach (HackableFile file in currentLoadedFiles.listOfFiles)
        {
            //Add any file names that match our search, or add all files to foundhacks
            if (file.Name.ToLower().Contains(inputString) || inputString == "all")
            {
                foundFiles.Add(file);
            }
        }
    }
    #region SearchFile
    public static void SearchFile(string inputString)
    {
        controller.currentActionLog.Add("Searching directory...");
        //Wait for dramatic purposes!
        tempString = inputString;
        GameController.RunFunctionAfterDelay(100, SearchFilePt2);
    }

    static void SearchFilePt2()
    {
        //UpdateFmod to appropriate intense levels
        hackingIntensity.setValue(3);

        SearchLibrary(tempString);

        if (foundFiles.Count == 0)
            controller.currentActionLog.Add("No results found");

        //Add each file name to the output log
        else
        {
            controller.currentActionLog.Add("Files found:" + "\n");

            foreach (HackableFile file in foundFiles)
            {
                string output = file.Name;
                controller.currentActionLog.Add("Name: " + output);
            }

            controller.LogStringWithReturn("");
        }

        //Clear the files we found for next use 
        foundFiles.Clear();
        //As enter hasn't been pressed, we need to manually called display logged text if we want any text to output from this function
        controller.DisplayLoggedText();
    }
    #endregion

    #region DisplayFile
    public static void DisplayFile(string inputString)
    {
        //Populate a list of files that match inputString
        foundFiles = new List<HackableFile>();

        foreach (HackableFile file in currentLoadedFiles.listOfFiles)
        {
            if (file.Name.ToLower().Contains(inputString))
            {
                foundFiles.Add(file);
            }
        }

        if (foundFiles.Count == 0)
            controller.LogStringWithReturn("No files with that name found");

        else if (foundFiles.Count == 1)
        {
            controller.LogStringWithReturn("Loading file...");

            //Wait for dramatic purposes!
            tempString = inputString;
            GameController.RunFunctionAfterDelay(100, DisplayFilePt2);
        }

        else if (foundFiles.Count > 1)
        {
            controller.LogStringWithReturn("Multiple files with this name found. Please specify file.");
        }
    }
    static void DisplayFilePt2()
    {
        //UpdateFmod to appropriate intense levels
        if (foundFiles[0].fileIsImportant)
        {
            importantFileFound = true;
            hackingIntensity.setValue(6);
        }
        else
            hackingIntensity.setValue(5);        

        SearchLibrary(tempString);
        //Call the 1st (and only) member of the foundhacks list (this function can only be called if foundfiles has one member)
        if (foundFiles[0].actionResponses.Length != 0 && !foundFiles[0].isDisplayFileWithActionResponse)
        {
            //If the file has an action response, do not display it
            controller.LogStringWithReturn("This file is not a datacluster that can be displayed, try running it instead.");
        }

        else
        {
            //Otherwise return the file's string and contents portions, adding them to the log. Run an action response if it has one too.

            for (int i = 0; i < foundFiles[0].actionResponses.Length; i++)
                foundFiles[0].actionResponses[i].DoActionResponse(controller);

            string output = foundFiles[0].Name;
            controller.LogStringWithReturn("Name: " + output);

            output = foundFiles[0].contents;
            controller.LogStringWithReturn(output);
        }

        //Clear the files we found for next use 
        foundFiles.Clear();
        //As enter hasn't been pressed, we need to manually called display logged text if we want any text to output from this function
        controller.DisplayLoggedText();
    }
    #endregion

    #region RunFile
    public static void RunFile(string inputString)
    {

        SearchLibrary(inputString);

        if (foundFiles.Count == 0)
            controller.currentActionLog.Add("No files with that name found");

        else if (foundFiles.Count == 1)
        {
            controller.currentActionLog.Add("Loading file..." + "\n");

            //Wait for dramatic purposes!
            GameController.RunFunctionAfterDelay(100, RunFilePt2);
        }

        else if (foundFiles.Count > 1)
        {
            controller.currentActionLog.Add("Multiple files with this name found. Please specify file.");
        }
    }

    static void RunFilePt2()
    {
        //Call the 1st (and only) member of the foundFiles list
        if (foundFiles[0].actionResponses.Length == 0)
        {
            //If the file is a not trigger, do not run it
            controller.currentActionLog.Add("This file is not a executable that can be run, try displaying it instead.");
        }

        else
        {
            //UpdateFmod to appropriate intense levels
            if (foundFiles[0].fileIsImportant)
            {
                importantFileFound = true;
                hackingIntensity.setValue(6);
            }
            else
                hackingIntensity.setValue(5);

            //run the file's DoActionResponse, as well as displaying an output message to let the player know it worked.
            string output = foundFiles[0].contents;
            controller.currentActionLog.Add(output);

            for (int i = 0; i < foundFiles[0].actionResponses.Length; i++)
                foundFiles[0].actionResponses[i].DoActionResponse(controller);
        }

        controller.LogStringWithReturn("");

        //Clear the files we found to get ready for next use of this function
        foundFiles.Clear();
        //As enter hasn't been pressed in this function (the script is delayed), we need to manually called display logged text if we want any text to output from this function
        controller.DisplayLoggedText();
    }
    #endregion
}