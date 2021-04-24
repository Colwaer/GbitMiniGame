using UnityEngine;
using Public;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal GameObject Player;         //控制的角色
    internal CPlayer m_Player;          //控制的角色的脚本
    internal Vector3 MousePos;          //鼠标位置
    internal Vector2 Direction;         //瞄准方向

    private bool b_DesiredJump = false;
    private bool b_DesiredShoot = false;

    bool b_IsActive = false;

    protected override void Awake()
    {
        base.Awake();
        Player = transform.Find("Player").gameObject;
        Debug.Log(Player.name);
        m_Player = Player.GetComponent<CPlayer>();
    }
    private void OnEnable()
    {
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.SceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!b_IsActive)
            return;
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
        if (!b_IsActive)
            return;
        m_Player.PhysicsCheck();
        m_Player.Move();
        if (b_DesiredJump)
        {
            b_DesiredJump = false;
            m_Player.Jump();
        }
        if (b_DesiredShoot)
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
        
        if (SceneIndex == 0)
        {
            b_IsActive = false;
            Player.SetActive(false);
        }
        else
        {
            b_IsActive = true;
            Player.SetActive(true);
        }
    }
}