using UnityEngine;
using Public;
using System.Collections;

public class CPlayer : MonoBehaviour
{
    internal float Speed { get; private set; }
    internal float JumpHeight { get; private set; }
    private float gravityScale;
    internal float GravityScale
    {
        get
        {
            if (m_RigidBody != null)
                return m_RigidBody.gravityScale;
            else
                return gravityScale;
        }
        private set
        {
            if (m_RigidBody != null)
                m_RigidBody.gravityScale = value;
            if (value > 0)
            {
                KeyCode_Jump = KeyCode.W;
                KeyCode_Swith = KeyCode.S;
            } 
            else
            {
                KeyCode_Jump = KeyCode.S;
                KeyCode_Swith = KeyCode.W;
            }
        }
    }
    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal float m_Direction;             //移动方向
    private float m_RaycastLength = 1.1f;
    internal KeyCode KeyCode_Jump, KeyCode_Swith;
    private void Awake()
    {
        Initialize();
    }

    public virtual void FixedUpdate()
    {
        PhysicsCheck();
        Move();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - m_RaycastLength * GravityScale, 0));
    }
    private void Initialize()
    {
        Speed = 3.0f;
        JumpHeight = 3.0f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        GravityScale = gravityScale = m_RigidBody.gravityScale;
        GroundLayer = LayerMask.GetMask("Ground");
    }

    private void PhysicsCheck()
    {
        b_IsMoving = m_RigidBody.velocity.magnitude > 0.05f;
        b_OnGround = Physics2D.Raycast(transform.position, new Vector2(0, -1 * gravityScale), m_RaycastLength, GroundLayer);
    }

    internal void Move()
    {
        m_RigidBody.velocity = new Vector2(m_Direction * Speed, m_RigidBody.velocity.y);
    }

    internal void Jump()
    {
        m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2) * GravityScale);
        Debug.Log(Mathf.Sqrt(JumpHeight * -Physics2D.gravity.y * 2) * GravityScale);
    }
}
