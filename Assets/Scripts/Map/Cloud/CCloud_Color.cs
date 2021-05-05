using UnityEngine;
using UnityEngine.Tilemaps;

public class CCloud_Color : CCloud
{
    public WindArea WindArea;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (!Active)
            return;
        if (other.collider.CompareTag("Player"))
        {
            if (PlayerController.Instance.m_Player.m_Velocity_LastFrame.magnitude > CollisionSpeed)
            {
                CEventSystem.Instance.CollideCloud?.Invoke();
                Active = false;
                WindArea.Count++;
            }
            else if (PlayerController.Instance.m_Player.OnGround)
                CEventSystem.Instance.CollideCloud?.Invoke();
        }
    }

    protected override void ResetCloud()
    {
        //不会被重新激活
    }
}
