using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单向移动的云
public class Cloud_Movable : MonoBehaviour
{
    public float Speed = 10f;
    public Vector2 Direction;
    private Vector3 OriginalPosition;
    protected bool b_Started;   //是否启动了移动
    protected GameObject Player;
    protected bool b_PlayerOnCloud;
    protected virtual void Awake()
    {
        Player = PlayerController.Instance.Player;
        OriginalPosition = transform.position;
    }
    protected virtual void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += ResetCloud;
    }
    protected virtual void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= ResetCloud;
    }

    protected virtual void ResetCloud()
    {
        StopAllCoroutines();
        transform.position = OriginalPosition;
        b_Started = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            b_PlayerOnCloud = true;
            if(!b_Started)
            {
                StartCoroutine(Move());
                b_Started = true;
            } 
        }
        else
        {
            StopAllCoroutines();
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            b_PlayerOnCloud = false;
        }
    }
    protected virtual IEnumerator Move()
    {
        yield return Public.CTool.Wait(1f);
        for (; ; )
        {
            Vector3 deltaPos = new Vector3(Direction.x, Direction.y, 0).normalized * Speed * Time.fixedDeltaTime;
            transform.position += deltaPos;
            if (b_PlayerOnCloud)
            {
                Player.transform.position += deltaPos;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
