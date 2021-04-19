using System.Collections.Generic;
using UnityEngine;
using Public;
using System;

//在此脚本刚生成的场景里，其他脚本执行Awake时此脚本可能还没生成
public class CEventSystem : CSigleton<CEventSystem>
{
    public Action<int> SceneLoaded;
}
