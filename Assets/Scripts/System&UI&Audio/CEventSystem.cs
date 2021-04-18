using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public class CEventSystem : CSigleton<CEventSystem>
{
    public Action<int> SceneLoaded;
}
