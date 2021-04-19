using UnityEngine;
using Public;
using System.Collections;
using System;

public class CPlayerController : CSigleton<CPlayerController>
{
    internal CPlayer m_Player;          //当前正在控制的角色的脚本
    internal Vector3 MousePos;          //鼠标位置
    internal Vector2 Direction;         //瞄准方向
    private float t_Shoot;              //射击冷却
    private bool b_Shoot;               //射击冷却完毕
    
    protected override void Awake()
    {
        base.Awake();
        Initialize();
        CEventSystem.Instance.SceneLoaded += OnSceneLoaded;
        OnSceneLoaded(1);    //以后需要修改   
    }

    public void Initialize()
    {
        b_Shoot = true;
        t_Shoot = 0.5f;
    }

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //按鼠标所在方向的反方向冲刺的代码有问题
        Direction =new Vector2(MousePos.x-transform.position.x,MousePos.y-transform.position.y).normalized;
        m_Player.m_Direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && m_Player.b_OnGround) 
            m_Player.Jump();
        if (Input.GetMouseButtonDown(0)&&b_Shoot)
        {
            Debug.Log(Direction);
            StartCoroutine(ShootCoolDown());
            m_Player.Shoot(Direction);
            StartCoroutine(m_Player.Dash((-1f)*Direction));
        }
    }

    private void FixedUpdate()
    {
        m_Player.PhysicsCheck();
        if (!m_Player.b_isDashing) m_Player.Move();
    }

    private void OnSceneLoaded(int SceneIndex)
    {
        if (SceneIndex != 0)
        {
            m_Player = GameObject.Find("Player").GetComponent<CPlayer>();
        }
    }

    private IEnumerator ShootCoolDown()
    {
        b_Shoot = false;
        yield return CTool.Wait(t_Shoot);
        b_Shoot = true;
    }
}