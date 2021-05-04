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
            if (other.collider.attachedRigidbody.velocity.magnitude > CollisionSpeed)
            {
                CEventSystem.Instance.TouchCloud?.Invoke(true);
                Active = false;
                WindArea.Count++;
            }
            else
                CEventSystem.Instance.TouchCloud?.Invoke(false);
        }
    }

    protected override void OnTouchCloud(bool isCollision)
    {
        //不会被重新激活
    }
}
