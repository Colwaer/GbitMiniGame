using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCloud : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            var t = GetComponentsInChildren<CSting_Movable>();
            foreach(CSting_Movable item in t)
            {
                item.StartMove();
            }
        }    
    }
}
