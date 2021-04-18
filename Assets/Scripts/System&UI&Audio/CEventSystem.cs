using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

public class CEventSystem : CSigleton<CEventSystem>
{
    public Action<int> SceneLoaded;
    public Action<int> ExchangePosition;     //正在操纵角色的编号
    public Action<int> Switched;             //正在操纵角色的编号
}
