using UnityEngine;
using Public;
using System.Collections;
using System;

public class CPlayer : MonoBehaviour
{
    internal float Speed { get; private set; }
    internal float DashSpeed { get; private set; } 
    internal float JumpHeight { get; private set; }

    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal bool b_isDashing;
    [SerializeField] 
    internal float m_DesiredDirection;      //移动方向
    private float m_RaycastLength = 1.2f;
    private float t_Dash;                   //冲刺时间,应当小于武器冷却

    [SerializeField] 
    private int frame_Accelerate;           //地面上加速需要的固定帧帧数（0->Speed，保持移动输入）
    [SerializeField] 
    private int frame_SlowDown;             //地面上减速需要的固定帧帧数（Speed->0，无移动输入）

    private float t_Shoot;                  //射击冷却
    private bool b_CanShoot;                //射击冷却完毕
    private IEnumerator ie_Dash;

    private event Action OnColl;            //碰撞事件

    private void Awake()
    {
        Initialize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - m_RaycastLength, 0));
    }

    public void Initialize()
    {
        frame_Accelerate = 20;
        frame_SlowDown = 10;
        b_CanShoot = true;
        t_Shoot = 0.5f;
        Speed = 10f;
        DashSpeed = 30f;
        JumpHeight = 3f;
        t_Dash = 0.3f;
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
            Debug.Log("stop dash");
            Debug.Log(m_RigidBody.velocity);
        }
            
    }

    public void PhysicsCheck()
    {
        b_IsMoving = m_RigidBody.velocity.magnitude > 0.1f;
        b_OnGround = Physics2D.Raycast(transform.position, new Vector2(0, -1), m_RaycastLength, GroundLayer);
    }
    // 地面跑动：采用惯性跑动，有加速与减速过程，但是不需要太明显弄得地面太滑；存在速度阈值，
    // 加速到阈值速度的时间大概在0.4s左右。减速的需求：减速从阈值到0速的时间为0.2s左右，只要
    // 玩家停止原运动方向的输入即判定进入减速阶段，减速过程位移与时间固定，玩家选择反向输入或
    // 停止输入不影响减速过程。但是玩家若在减速过程中选择原方向输入，那就以当前的速度进入到加速过程。

    [SerializeField]
    float v;    //监视速度
    public void GroundMove()
    {
        v = m_RigidBody.velocity.x;
        if (b_isDashing) return;
        float vx = Mathf.Abs(m_RigidBody.velocity.x);
        float sgn = Mathf.Sign(m_RigidBody.velocity.x);   //为什么物体静止时sgn=1呢？
        //速度很小时想往哪边移就往哪边移，所以此时把sgn赋值成移动输入的方向，不可以赋值为0，原因见下方代码
        if (vx < 0.01f) sgn = m_DesiredDirection;   

        if (vx > Speed)
        {
            //地面上的水平速度不能超过Speed
            if (b_OnGround) m_RigidBody.velocity =
                new Vector2(sgn * Speed, m_RigidBody.velocity.y);
            //空中水平速度超过Speed则不能再加速（但可以减速）,空中减速暂时不需要
            else if (sgn * m_DesiredDirection > 0)
                return;
        }

        //m_DesiredDirection * sgn>0表明移动输入和速度同向
        m_RigidBody.velocity = new Vector2(sgn * Mathf.Min(Speed, vx + Speed / frame_Accelerate * m_DesiredDirection * sgn), m_RigidBody.velocity.y); 
        
        //移动输入和速度同向时,不执行减速，这样的话加速时间（0到Speed）等于t_Accelerate
        //无移动输入时，减速时间（Speed到0）等于t_SlowDown
        //移动输入和速度反向时，减速时间（Speed到0）小于t_SlowDown
        if (m_DesiredDirection * sgn <= 0)
            m_RigidBody.velocity = new Vector2(sgn * Mathf.Max(0,vx - Speed / frame_SlowDown), m_RigidBody.velocity.y);
    }

    /*
    public void Move()
    {
        if (b_isDashing) return;
        if (m_DesiredPosition > 0.05f)
        {
            if (m_RigidBody.velocity.x <= Speed)
            {
                Debug.Log(new Vector2(m_RigidBody.mass * Speed / accelerateTime, 0));
                m_RigidBody.AddForce(new Vector2(m_RigidBody.mass * Speed / accelerateTime + Speed * 5, 0), ForceMode2D.Force);
            }
        }
        else if (m_DesiredPosition < -0.05f)
        {
            if (m_RigidBody.velocity.x >= -Speed)
            {
                Debug.Log(-new Vector2(m_RigidBody.mass * Speed / accelerateTime, 0));
                m_RigidBody.AddForce(-new Vector2(m_RigidBody.mass * Speed / accelerateTime + Speed * 5, 0), ForceMode2D.Force);
            }
        }
        else
        {
                
            if (Mathf.Abs(m_RigidBody.velocity.x) > 0.5f)
            {
                m_RigidBody.AddForce(new Vector2((m_RigidBody.velocity.x > 0 ? -1f : 1f) * m_RigidBody.mass * Speed / (accelerateTime * 2), 0), ForceMode2D.Force);
            }
            else
            {
                m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);
            }
        }
    }
     */

    public void Jump()
    {
        if (b_OnGround)
            m_RigidBody.velocity =  new Vector2(m_RigidBody.velocity.x, Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2));
    }

    public void Shoot(Vector2 direction)
    {
        if (!b_CanShoot)
            return;
        ie_Dash = Dash(direction);
        StartCoroutine(ie_Dash);
        StartCoroutine(ShootCoolDown());
    }
    //冲刺时不能主动移动，也不受重力影响
    public IEnumerator Dash(Vector2 direction)
    {
        b_isDashing = true;
        m_RigidBody.gravityScale = 0;
        m_RigidBody.velocity = direction * DashSpeed;
        //Debug.Log(direction);
        yield return CTool.Wait(t_Dash);
        b_isDashing = false;
        m_RigidBody.gravityScale = 1;
        m_RigidBody.velocity = Vector2.zero;
    }
    private IEnumerator ShootCoolDown()
    {
        b_CanShoot = false;
        yield return new WaitForSeconds(t_Shoot);
        b_CanShoot = true;
    }

}
