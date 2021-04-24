using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("player collider with cloud");
            var t = GetComponentsInChildren<MoveableSting>();
            foreach(MoveableSting item in t)
            {
                item.StartMove();
            }
        }    
    }
}
