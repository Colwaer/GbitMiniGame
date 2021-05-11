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
    private Collider2D PlayerColl;

    protected virtual void Awake()
    {
        PlayerColl = PlayerController.Instance.Player.GetComponent<Collider2D>();
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

    protected void ResetCloud()
    {
        StopAllCoroutines();
        transform.position = OriginalPosition;
        b_Started = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
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

    protected virtual IEnumerator Move()
    {
        for (; ; )
        {
            Vector3 deltaPos = new Vector3(Direction.x, Direction.y, 0).normalized * Speed * Time.fixedDeltaTime;
            transform.position += deltaPos;
            if (Physics2D.IsTouchingLayers(PlayerColl))
                PlayerColl.gameObject.transform.position += deltaPos;
            yield return new WaitForFixedUpdate();
        }
    }
}
