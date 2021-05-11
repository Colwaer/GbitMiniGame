using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSting_ComeBack : CSting_Movable
{
    //在两朵云间反复移动（人物触碰云使刺移动）
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player") && other.transform != transform.parent)
        {
            StopAllCoroutines();
            transform.SetParent(other.transform);
            Direction = -Direction;
        }    
    }
}
