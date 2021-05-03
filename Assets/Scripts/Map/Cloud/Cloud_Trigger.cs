using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Trigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Public.IPlayer player = other.gameObject.GetComponent<Public.IPlayer>();
        if (player != null)
        {
            var t = GetComponentsInChildren<CSting_Movable>();
            foreach(CSting_Movable item in t)
            {
                item.StartMove();
            }
        }    
    }
}
