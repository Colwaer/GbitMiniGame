using UnityEngine;
using Public;
using System.Collections;
using System.Collections.Generic;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal CPlayer m_Player;           //当前正在控制的角色的脚本

    protected override void Awake()
    {
        base.Awake();
        OnSceneLoaded(1);    //以后需要修改               
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        m_Player.m_Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && m_Player.b_OnGround) 
            m_Player.Jump();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnSceneLoaded(int index)
    {
        if (index != 0)
        {
            m_Player = GameObject.Find("Player").GetComponent<CPlayer>();
        }
    }
}