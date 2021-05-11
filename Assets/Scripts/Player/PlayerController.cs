using Public;
using System.Collections;
using UnityEngine;

public class PlayerController : Sigleton<PlayerController>
{
    private bool b_IsActive = false;            //是否启用控制器

    internal GameObject Pointer;        //辅助瞄准的箭头
    internal GameObject Player;         //控制的角色
    internal CPlayer m_Player;          //控制的角色的脚本
    internal Vector3 MousePos;          //鼠标位置
    internal Vector2 Direction;         //瞄准方向

    internal float t_ControlDirection = 1f;       //冲刺前的最大瞄准时间

    internal bool b_DesiredJump = false;
    internal bool b_DesiredShoot = false;

    [SerializeField] private bool b_TestMode;

    protected override void Awake()
    {
        base.Awake();
        Player = transform.Find("Player").gameObject;
        Pointer = transform.Find("Pointer").gameObject;
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
        // Debug.Log("GetMask : " + LayerMask.GetMask("Ground") + "Name to Mask : " + LayerMask.NameToLayer("Ground"));
        if (b_TestMode)  //调试用
        {
            if (Input.GetMouseButtonDown(1))
                m_Player.ShootCount = 3;
            else if (Input.GetMouseButtonDown(2))
                CSceneManager.Instance.LoadNextLevel();
        }

        if (!b_IsActive)
            return;

        CalculateMouseDirection();
        m_Player.m_DesiredDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            b_DesiredJump = true;
        }
            

        if (Input.GetMouseButtonDown(0) && m_Player.ShootCount > 0)
        {
            Pointer.SetActive(true);
            Time.timeScale = 0.5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Pointer.SetActive(false);
            Time.timeScale = 1f;
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
            m_Player.Shoot(Direction);
        }
        m_Player.SwitchAnim();
    }
    
    public void PauseControl(int time)
    {

    }
    private IEnumerator PauseControl_(int time)
    {
        m_Player.m_RigidBody.velocity = Vector2.zero;
        b_IsActive = false;
        yield return CTool.Wait(time);
        b_IsActive = true;
    }
    
    private void CalculateMouseDirection()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
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