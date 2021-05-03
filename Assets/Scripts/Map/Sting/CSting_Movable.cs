using System.Collections;
using UnityEngine;

public class CSting_Movable : CSting
{
    public float Speed = 20f;
    public Vector2 direction;

    public virtual void StartMove()
    {
        StartCoroutine(Move());
    }

    protected virtual IEnumerator Move()
    {
        for (; ; )
        {
            transform.position += new Vector3(direction.x, direction.y, 0).normalized * Speed * Time.deltaTime;
            yield return null;
        }
    }
}
