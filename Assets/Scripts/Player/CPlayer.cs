using UnityEngine;
using Public;
using System.Collections;

public class CPlayer : MonoBehaviour
{
    internal float Speed { get; private set; }
    internal float JumpHeight { get; private set; }

    private LayerMask GroundLayer;
    internal Rigidbody2D m_RigidBody;
    internal bool b_OnGround;
    internal bool b_IsMoving;
    internal float m_Direction;             //移动方向
    private float m_RaycastLength = 1.2f;   

    private void Awake()
    {
        Initialize();
    }

    public void FixedUpdate()
    {
        PhysicsCheck();
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - m_RaycastLength, 0));
    }

    protected void Initialize()
    {
        Speed = 4.0f;
        JumpHeight = 3.0f;
        m_RigidBody = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
    }

    protected void PhysicsCheck()
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

}
