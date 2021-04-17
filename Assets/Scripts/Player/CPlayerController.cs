using UnityEngine;
using Public;
using System.Collections;
using System.Collections.Generic;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal List<GameObject> players;
    private int m_playerIndex = 0;
    internal CPlayer m_Player;           //当前正在控制的角色的脚本




    protected override void Awake()
    {
        base.Awake();
        players = new List<GameObject>();
        OnSceneLoaded(1);
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        //在这里把输入传给m_Player
        m_Player.m_Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.S))
            Switch();
        if (Input.GetButtonDown("Jump") && m_Player.b_OnGround) 
            m_Player.Jump();
    }
    private void FixedUpdate()
    {
        //调用m_Player的fixedupdate
        m_Player.FixedUpdate();
       
    }
    private void Switch()
    {
        if (m_playerIndex == 0)
            m_playerIndex = 1;
        else
            m_playerIndex = 0;
        m_Player = players[m_playerIndex].GetComponent<CPlayer>();
    }
    private void OnSceneLoaded(int index)
    {
        if(index!=0)
        {

            players.Add(GameObject.Find("player1"));
            players.Add(GameObject.Find("player2"));
            m_Player = players[0].GetComponent<CPlayer>();
        }
    }
}