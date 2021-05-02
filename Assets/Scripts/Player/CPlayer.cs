using UnityEngine;
using Public;
using System.Collections;
using System;
using static PlayerData;

public class CPlayer : MonoBehaviour, IPlayer
{
    internal float Speed { get; private set; }
    internal float DashSpeed { get; private set; }
    internal float JumpHeight { get; private set; }
    private float MaxFallSpeed;
    private float MaxRiseSpeed;

    private float t_Dash;                   //冲刺时间,应当小于射击冷却
    private int frame_Accelerate;           //地面上加速需要的固定帧帧数 
    private int frame_SlowDown;             //地面上减速需要的固定帧帧数
    private float t_Shoot;                  //射击冷却时间

    internal int MaxShootCount;             //最大射击次数
    [SerializeField]
    private int _ShootCount;
    internal int ShootCount                 //可用射击次数
    {
        get
        {
            return _ShootCount;
        }
        set
        {
            if (value > MaxShootCount) return;
            CEventSystem.Instance.ShootCountChanged?.Invoke(value);
            _ShootCount = value;
        }
    }

    [SerializeField]
    private int _Point;
    internal int Point
    {
        get
        {
            return _Point;
        }
        set
        {
            CEventSystem.Instance.ShootCountChanged?.Invoke(value);
            _Point = value;
        }
    }

    [SerializeField] private Animator PlayerAnim;
    [SerializeField] private Animator BottleAnim;
    private GameObject LastCloud;           //上一朵碰撞的云
    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;
    private float RaycastLength = 1.2f;
    private Vector3 RaycastOffset = new Vector3(0.5f, 0);
    private Coroutine ie_Dash;            //冲刺协程

    [Header("状态")]
    [SerializeField] private int statusindex;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal bool b_isDashing;
    private bool b_CanShoot;                //射击冷却完毕
    internal float m_DesiredDirection;
    private Vector2 m_Velocity_LastFrame;   //上一固定帧中的速度
    private float v_x;
    private float v_y;
    private float sgn_x;
    private float sgn_y;

    private void Awake()
    {
        Initialize();
    }

    private void OnDrawGizmos()
    {
        Vector3 pos1 = transform.position + RaycastOffset;
        Vector3 pos2 = transform.position - RaycastOffset;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1, new Vector3(pos1.x, pos1.y - RaycastLength, 0)); 
        Gizmos.DrawLine(pos2, new Vector3(pos2.x, pos2.y - RaycastLength, 0));
    }

    public void Initialize()
    {
        Point = 0;
        frame_Accelerate = 13;  //最好与起跑动画时间一致
        frame_SlowDown = 10;
        MaxShootCount = 3;
        ShootCount = 0;
        b_CanShoot = true;
        t_Shoot = 0.3f;
        Speed = 10f;
        DashSpeed = 30f;
        MaxFallSpeed = 10f;
        MaxRiseSpeed = 20f;
        JumpHeight = 3f;
        t_Dash = 0.15f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (b_isDashing && ie_Dash != null)
        {
            StopCoroutine(ie_Dash);
            b_isDashing = false;
            m_RigidBody.gravityScale = 1;
        }

        //不能通过连续撞击同一朵云来获得云，只要接触了另一朵云，就解除这个限制
        if (LastCloud == null || collision.gameObject != LastCloud)
        {
            if (m_Velocity_LastFrame.magnitude > 15f)
            {
                ShootCount += 2;
                LastCloud = collision.gameObject;
                //向下冲锋撞击云只能获得一朵云（依然有不能连续获得的限制）
                if (m_Velocity_LastFrame.y < 0)
                    ShootCount--;
            }
            else
                LastCloud = null;
        }
    }

    public void PhysicsCheck()
    {
        m_Velocity_LastFrame = m_RigidBody.velocity;
        v_x = Mathf.Abs(m_Velocity_LastFrame.x);
        v_y = Mathf.Abs(m_Velocity_LastFrame.y);
        sgn_x = Mathf.Sign(m_Velocity_LastFrame.x);
        sgn_y = Mathf.Sign(m_Velocity_LastFrame.y);
        if (v_x < 0.1f) sgn_x = m_DesiredDirection;    //禁止赋值为0
        if (v_y < 0.1f) sgn_y = -1;                    //禁止赋值为0

        b_IsMoving = v_x > 0.1f && b_OnGround && sgn_x * m_DesiredDirection > 0;

        b_OnGround = Physics2D.Raycast(transform.position + RaycastOffset, new Vector2(0, -1), RaycastLength, GroundLayer)
           || Physics2D.Raycast(transform.position - RaycastOffset, new Vector2(0, -1), RaycastLength, GroundLayer);

        if (b_OnGround) ShootCount = 1;
    }

    public void Move()
    {
        if (b_isDashing)
            return;

        if (b_OnGround)
        {
            m_RigidBody.velocity = new Vector2(sgn_x * Mathf.Min(Speed, v_x + Speed / frame_Accelerate * m_DesiredDirection * sgn_x), m_RigidBody.velocity.y);
            //地面上的水平速度不能超过Speed
            if (v_x > Speed)
                m_RigidBody.velocity = new Vector2(sgn_x * Speed, m_RigidBody.velocity.y);
            if (m_DesiredDirection * sgn_x <= 0)
                m_RigidBody.velocity = new Vector2(sgn_x * Mathf.Max(0, v_x - Speed / frame_SlowDown), m_RigidBody.velocity.y);
        }
        else
        {
            if (sgn_y < 0 && v_y > MaxFallSpeed)
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, -MaxFallSpeed);
            else if (sgn_y > 0 && v_y > MaxRiseSpeed)
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, MaxRiseSpeed);
            //空中水平速度超过Speed则不能再加速（但可以减速）
            if (v_x < Speed || sgn_x * m_DesiredDirection < 0)
                m_RigidBody.velocity = new Vector2(sgn_x * Mathf.Min(Speed, v_x + Speed / frame_Accelerate * m_DesiredDirection * sgn_x), m_RigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        if (!b_OnGround) 
            return;
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2));
    }

    public void Shoot(Vector2 direction)
    {
        if (ShootCount <= 0)
            return;
        ShootCount--;
        if (!b_CanShoot)
            return;
        ie_Dash =StartCoroutine(Dash(direction));
        StartCoroutine(ShootCoolDown());
    }

    private IEnumerator ShootCoolDown()
    {
        b_CanShoot = false;
        yield return new WaitForSeconds(t_Shoot);
        b_CanShoot = true;
    }
    //冲刺时不能主动移动，也不受重力影响
    public IEnumerator Dash(Vector2 direction)
    {
        b_isDashing = true;
        m_RigidBody.gravityScale = 0;
        m_RigidBody.velocity = direction * DashSpeed;
        yield return CTool.Wait(t_Dash);
        //减速过程，时间很短
        for (; m_RigidBody.velocity.magnitude > Speed;)
        {
            m_RigidBody.velocity -= m_RigidBody.velocity.normalized * 2f;
            yield return new WaitForFixedUpdate();
        }
        b_isDashing = false;
        m_RigidBody.gravityScale = 1;
    }

    public void Die()
    {
        m_RigidBody.velocity = Vector2.zero;
        ShootCount = 0;
        CEventSystem.Instance.PlayerDie?.Invoke();
    }
    //statusindex是动画器参数，0对应idle，1对应startwalk，2对应walk,3对应jump，4对应drop,5对应dash
    public void SwitchAnim()
    {
        float animSpeed = 1f; 

        //改变的是父物体而不是图片的Scale
        if(sgn_x!=0)
            transform.localScale = new Vector3(-sgn_x, 1, 1);

        if(b_isDashing)
        {
            statusindex = 5;
        }
        else if(b_OnGround)
        {
            if (v_x == Speed) statusindex = 2;
            else if (b_IsMoving) statusindex = 1;
            else statusindex = 0;
        }
        else
        {
            if (sgn_y > 0) statusindex = 3;
            else statusindex = 4;
            animSpeed = (float)(Mathf.Sqrt(m_Velocity_LastFrame.magnitude) / 4);
        }
       
        PlayerAnim.SetInteger("statusindex", statusindex);
        BottleAnim.SetInteger("statusindex", statusindex);
        PlayerAnim.speed = animSpeed;
        BottleAnim.speed = animSpeed;
    }
}
