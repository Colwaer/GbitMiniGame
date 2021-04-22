﻿using UnityEngine;
using Public;
using System.Collections;
using System;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal CPlayer m_Player;          //当前正在控制的角色的脚本
    internal Vector3 MousePos;          //鼠标位置
    internal Vector2 Direction;         //瞄准方向

    private bool b_DesiredJump = false;
    private bool b_DesiredShoot = false;

    public void Initialize()
    {
        OnSceneLoaded(1);
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {    
        CalculateDirection();

        m_Player.m_DesiredDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            b_DesiredJump = true;
        
        if (Input.GetMouseButtonDown(0))
        {
            b_DesiredShoot = true;       
        }
    }

    private void FixedUpdate()
    {
        m_Player.PhysicsCheck();
        m_Player.Move();
        if (b_DesiredJump)
        {
            b_DesiredJump = false;
            m_Player.Jump();
        }
        if (b_DesiredShoot&&m_Player.ShootCount>0)
        {
            b_DesiredShoot = false;
            m_Player.Shoot(-1f * Direction);
        }
    }
    private void CalculateDirection()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //按鼠标所在方向的反方向冲刺的代码有问题
        Direction = new Vector2(MousePos.x - m_Player.transform.position.x, MousePos.y - m_Player.transform.position.y).normalized;
    }
    private void OnSceneLoaded(int SceneIndex)
    {
        if (SceneIndex > 0)
        {
            m_Player = GameObject.Find("Player").GetComponent<CPlayer>();
        }
    }
}