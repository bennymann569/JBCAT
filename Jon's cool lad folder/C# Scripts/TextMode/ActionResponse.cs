﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject

{
    public Room roomToBeIn;

    public abstract bool DoActionResponse(GameController controller); 
}
