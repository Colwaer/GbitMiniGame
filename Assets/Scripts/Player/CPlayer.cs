using UnityEngine;
using Public;
using System.Collections;
using System;

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
            CEventSystem.Instance.PointChanged?.Invoke(value);
            _Point = value;
        }
    }

    [SerializeField] private Animator PlayerAnim;
    [SerializeField] private Animator BottleAnim;
    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;

    private float RaycastLength_Ground = 1.15f;
    const float OriginRaycastLength_Ground = 1.15f;
    private float RaycastLength_CloseToGround = 3.5f;

    private Vector3 RaycastOffset = new Vector3(0.4f, 0);
    private Coroutine ie_Dash;            //冲刺协程

    [Header("状态")]
    [SerializeField] private int statusindex;
    [SerializeField] private bool _OnGround;
    internal bool OnGround
    {
        get
        {
            return _OnGround;
        }
        set
        {

            if (_OnGround == false && value == true && !b_isDashing)
            {
                RaycastLength_Ground = 1.5f;
            }
                
            _OnGround = value;
            

            if (value)
            {
                ShootCount = 1;
                CEventSystem.Instance.TouchGround?.Invoke();
            }
            else
            {
                RaycastLength_Ground = OriginRaycastLength_Ground;
            }
        }
    }
    internal bool b_CloseToGround = false;  //仅用于动画
    internal bool b_IsMoving = false;
    internal bool b_isDashing = false;
    internal bool b_CanShoot = true;          //射击冷却完毕
    internal float m_DesiredDirection;
    public Vector2 m_Velocity_LastFrame;    //上一固定帧中的速度
    private float v_x;
    private float v_y;
    private float sgn_x;
    private float sgn_y;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        CEventSystem.Instance.CollideCloud += OnCollideCloud;
        CEventSystem.Instance.TouchGround += OnTouchGround;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.CollideCloud -= OnCollideCloud;
        CEventSystem.Instance.TouchGround -= OnTouchGround;
    }

    public void Initialize()
    {
        Point = 0;
        frame_Accelerate = 10;  
        frame_SlowDown = 10;
        MaxShootCount = 3;
        ShootCount = 0;
        t_Shoot = 0.3f;
        Speed = 10f;
        DashSpeed = 30f;
        MaxFallSpeed = 10f;
        MaxRiseSpeed = 20f;
        JumpHeight = 3.5f;
        t_Dash = 0.15f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopDash();
    }

    private void OnCollideCloud()
    {
        ShootCount += 2;
    }
    private void OnTouchGround()
    {
        ShootCount = 1;
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

        b_IsMoving = v_x > 0.1f && OnGround && sgn_x * m_DesiredDirection > 0;

        OnGround = Physics2D.Raycast(transform.position + RaycastOffset, new Vector2(0, -1), RaycastLength_Ground, GroundLayer)
           || Physics2D.Raycast(transform.position - RaycastOffset, new Vector2(0, -1), RaycastLength_Ground, GroundLayer);

        b_CloseToGround = Physics2D.Raycast(transform.position + RaycastOffset, new Vector2(0, -1), RaycastLength_CloseToGround, GroundLayer)
           || Physics2D.Raycast(transform.position - RaycastOffset, new Vector2(0, -1), RaycastLength_CloseToGround, GroundLayer);
    }

    public void Move()
    {
        if (b_isDashing)
            return;

        int accelarateDirection; //0表示无输入，1表示水平输入与当前移动方向一致，-1表示相反
        if (m_DesiredDirection == 0) accelarateDirection = 0;
        else if (m_DesiredDirection * sgn_x > 0) accelarateDirection = 1;
        else accelarateDirection = -1;

        if (OnGround)
        {
            //如果不希望加速，地面上会自然减速
            if (accelarateDirection <= 0)
            {
                v_x -= Speed / frame_SlowDown;
                if (v_x < 0) v_x = 0;
            }
        }
        else
        {
            if (sgn_y < 0 && v_y > MaxFallSpeed)
                v_y = MaxFallSpeed;
            else if (sgn_y > 0 && v_y > MaxRiseSpeed)
                v_y = MaxRiseSpeed;
        }
        //水平速度不能超过Speed
        v_x += accelarateDirection * Speed / frame_Accelerate;
        if (v_x > Speed) v_x = Speed;

        m_RigidBody.velocity = new Vector2(sgn_x * v_x,sgn_y * v_y);
    }

    public void Jump()
    {
        Debug.Log(RaycastLength_Ground.ToString() + " "  + OnGround.ToString());

        if (!OnGround) 
            return;

        RaycastLength_Ground = OriginRaycastLength_Ground;
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2));
    }
    //direction是喷射方向，也就是冲刺的反方向
    public void Shoot(Vector2 direction)
    {
        if (ShootCount <= 0)
            return;
        ShootCount--;
        if (!b_CanShoot)
            return;
        ie_Dash = StartCoroutine(Dash(-direction));
        CEventSystem.Instance.PlayerShoot?.Invoke();
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
        RaycastLength_Ground = OriginRaycastLength_Ground;
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
        StopDash();
    }
    public void StopDash()
    {
        if (b_isDashing && ie_Dash != null) StopCoroutine(ie_Dash);
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
        //改变的是所在物体而不是图片的Scale
        if (v_x > 3.1f)
            transform.localScale = new Vector3(-sgn_x, 1, 1);
        else if (m_DesiredDirection != 0)
            transform.localScale = new Vector3(-m_DesiredDirection, 1, 1);

        if (b_isDashing)
        {
            statusindex = 5;
        }
        else if (OnGround)
        {
            if (v_x > 8f) statusindex = 2; 
            else if (b_IsMoving) statusindex = 1;
            else statusindex = 0;
        }
        else
        {
            if (sgn_y > 0) statusindex = 3;
            else if (b_CloseToGround)
                statusindex = 0;    //播放落地动画
            else
                statusindex = 4;
        }
        
        PlayerAnim.SetInteger("statusindex", statusindex);
        BottleAnim.SetInteger("statusindex", statusindex);
    }
    public void SetAnimSpeed(float speed)
    {
        PlayerAnim.speed = speed;
        BottleAnim.speed = speed;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos0 = transform.position;
        Vector3 pos1 = pos0 + RaycastOffset;
        Vector3 pos2 = pos0 - RaycastOffset;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1, new Vector3(pos1.x, pos1.y - RaycastLength_Ground, 0));
        Gizmos.DrawLine(pos2, new Vector3(pos2.x, pos2.y - RaycastLength_Ground, 0));
    }

}
