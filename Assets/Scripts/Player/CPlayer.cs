using UnityEngine;
using Public;
using System.Collections;

public class CPlayer : MonoBehaviour
{
    internal float Speed { get; private set; }
    internal float JumpSpeed { get; private set; }
    internal float GravityScale
    {
        get => m_RigidBody.gravityScale;
        private set
        {
            m_RigidBody.gravityScale = value;
        }
    }

    internal Rigidbody2D m_RigidBody;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal float m_Directiion;             //移动方向

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        Move();
    }

    private void Initialize()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void PhysicsCheck()
    {
        b_IsMoving = m_RigidBody.velocity.magnitude > 0.1f;
    }

    internal void Move()
    {
        m_RigidBody.velocity = new Vector2(m_Directiion * Speed, m_RigidBody.velocity.y);
    }

    internal void Jump()
    {
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, JumpSpeed * GravityScale);
    }
}
