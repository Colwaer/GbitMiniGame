using UnityEngine;
using Public;
using System.Collections;
using System;

public class CPlayer : MonoBehaviour
{
    internal float Speed { get; private set; }
    internal float DashSpeed { get; private set; }                //冲刺速度
    internal float JumpHeight { get; private set; }

    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal bool b_isDashing;
    internal float m_Direction;             //移动方向
    private float m_RaycastLength = 1.2f;
    private float t_Dash;                   //冲刺时间,应当小于武器冷却
    private float m_E;                      //恢复系数
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
        Speed = 6f;
        DashSpeed = 30f;
        JumpHeight = 3f;
        t_Dash = 0.3f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnColl?.Invoke();
    }

    public void PhysicsCheck()
    {
        b_IsMoving = m_RigidBody.velocity.magnitude > 0.05f;
        b_OnGround = Physics2D.Raycast(transform.position, new Vector2(0, -1), m_RaycastLength, GroundLayer);
    }

    public void Move()
    {
        m_RigidBody.velocity = new Vector2(m_Direction * Speed, m_RigidBody.velocity.y);
    }

    public void Jump()
    {
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2));
    }

    public void Shoot(Vector2 direction)
    {
        
    }
    //冲刺时不能主动移动，也不受重力影响
    public IEnumerator Dash(Vector2 direction)
    {
        b_isDashing = true;
        m_RigidBody.gravityScale = 0;
        m_RigidBody.velocity = direction * DashSpeed;
        Debug.Log(direction);
        yield return CTool.Wait(t_Dash);
        b_isDashing = false;
        m_RigidBody.gravityScale = 1;
        m_RigidBody.velocity = Vector2.zero;
    }
}
