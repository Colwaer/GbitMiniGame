using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public class CEventSystem : CSigleton<CEventSystem>
{
    public Action<int> SceneLoaded;
    public Action<bool> Switched;       //true表示切换到player1，false表示切换到角色player1
}
