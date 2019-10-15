using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HackableFile
{
    public string Name;
    [TextArea]
    public string contents;
    public ActionResponse[] actionResponses;
    public bool fileIsImportant;
    public bool isDisplayFileWithActionResponse = false;
}
