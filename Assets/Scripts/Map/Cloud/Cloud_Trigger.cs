using UnityEngine;

public class Cloud_Trigger : CCloud
{
    protected override void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            var t = GetComponentsInChildren<CSting_Movable>();
            foreach(CSting_Movable item in t)
            {
                item.StartMove();
            }
        }
        base.OnCollisionEnter2D(other);
    }
}
