using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//往复运动的云
public class Cloud_ComeBack : Cloud_Movable
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            b_PlayerOnCloud = true;
            if (!b_Started)
            {
                StartCoroutine(Move());
                b_Started = true;
            }
        }
        else
        {
            StopAllCoroutines();
            Direction *= -1;
            StartCoroutine(Move());
        }
    }
    //不会被重置
    protected override void ResetCloud()
    {
        
    }
}
