using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/FileLibrary")]

public class FileLibrary : ScriptableObject
{
    public List<HackableFile> listOfFiles = new List<HackableFile>();
}
