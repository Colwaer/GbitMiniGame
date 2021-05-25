using System.Collections.Generic;
using UnityEngine;
using Public;
using System;
using System.Collections;

//此脚本所属游戏物体应当被最先生成
public class CEventSystem : Singleton<CEventSystem>
{
    public Action<int> SceneLoaded;
    public Action ScenePassed;              //通过了当前场景
    public Action<int> ShootCountChanged;   //射击次数改变了
    public Action PlayerDie;
    public Action CheckPointChanged;        //激活了新的记录点
    public Action<int> PointChanged;
    public Action CollideCloud;
    public Action TouchGround;
    public Action PlayerShoot;
    protected override void Awake()
    {
        base.Awake();
        ScenePassed += Random_Background.Reset;
    }
}

