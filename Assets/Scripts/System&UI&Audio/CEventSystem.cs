using System.Collections.Generic;
using UnityEngine;
using Public;
using System;
using System.Collections;

//此脚本所属游戏物体应当被最先生成
public class CEventSystem : CSigleton<CEventSystem>
{
    public Action<int> SceneLoaded;
    public Action<int> ShootCountChanged;   //射击次数改变了
    public Action PlayerDie;
    public Action CheckPointChanged;        //激活了新的记录点

    protected override void Awake()
    {
        base.Awake();
        UnityEngine.Random.InitState(System.DateTime.Now.Second);
    }
}

