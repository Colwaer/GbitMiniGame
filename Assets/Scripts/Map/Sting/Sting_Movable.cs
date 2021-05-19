using System.Collections;
using UnityEngine;

//已弃用
public class Sting_Movable : Sting
{
    public float Speed;
    public Vector2 Direction;

    public virtual void StartMove()
    {
        StartCoroutine(Move());
    }

    protected virtual IEnumerator Move()
    {
        for (; ; )
        {
            transform.position += new Vector3(Direction.x, Direction.y, 0).normalized * Speed * Time.deltaTime;
            yield return null;
        }
    }
}
