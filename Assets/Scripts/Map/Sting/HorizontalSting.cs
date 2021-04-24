using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSting : MoveableSting
{
    public float speed = 3.0f;
    public Vector2 direction;

    public override void StartMove()
    {
        Debug.Log("start move");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        for(;;)
        {
            transform.position += new Vector3(direction.x, direction.y, 0).normalized * speed * Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag != "Player" && other.transform != transform.parent)
        {
            Debug.Log("sting collide with cloud");
            StopAllCoroutines();
            transform.SetParent(other.transform);
            direction = -direction;
        }    
    }
}
