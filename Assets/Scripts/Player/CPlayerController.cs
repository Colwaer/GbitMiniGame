using UnityEngine;
using Public;
using System.Collections;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal GameObject Player1, Player2;
    internal CPlayer m_Player;           //当前正在控制的角色的脚本
    
    private void Start()
    {
        OnSceneLoaded(1);
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        m_Player.m_Directiion = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")&&m_Player.b_OnGround) m_Player.Jump();
    }

    private void OnSceneLoaded(int index)
    {
        if(index!=0)
        {
            Player1 = GameObject.Find("player1");
            Player2 = GameObject.Find("player2");
            m_Player = Player1.GetComponent<CPlayer>();
        }
    }
}