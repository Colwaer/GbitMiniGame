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
        OnSceneLoaded(1);    //以后需要修改               
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        m_Player.m_Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(m_Player.KeyCode_Swith))
            Switch();
        else if (Input.GetKeyDown(m_Player.KeyCode_Jump) && m_Player.b_OnGround) //else不能删
            m_Player.Jump();
        if (Input.GetKeyDown(KeyCode.Q))
            ExchangePosition();
    }

    private void FixedUpdate()
    {
        m_Player.ControlledFixedUpdate();
    }

    private void Switch()
    {
        m_Player.m_RigidBody.velocity = Vector2.zero;
        m_playerIndex = 1- m_playerIndex;
        m_Player = players[m_playerIndex].GetComponent<CPlayer>();
        CEventSystem.Instance.Switched?.Invoke(m_playerIndex);
    }

    private void ExchangePosition()
    {
        Vector3 tempPos = players[m_playerIndex].transform.position;
        players[m_playerIndex].transform.position = players[1-m_playerIndex].transform.position;
        players[1 - m_playerIndex].transform.position = tempPos;
        m_Player.Twist();
        players[1 - m_playerIndex].GetComponent<CPlayer>().Twist();
        CEventSystem.Instance.Switched?.Invoke(m_playerIndex);
    }

    private void OnSceneLoaded(int index)
    {
        if(index!=0)
        {
            players.Clear();
            players.Add(GameObject.Find("player0"));
            players.Add(GameObject.Find("player1"));
            m_Player = players[0].GetComponent<CPlayer>();
        }
    }
}