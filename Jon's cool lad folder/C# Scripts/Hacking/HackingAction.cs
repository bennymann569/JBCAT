using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HackingAction : ScriptableObject
{
    public string keyWord;

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);
}
