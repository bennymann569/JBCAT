using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public List<string> keyWords = new List<string>();

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);
}
