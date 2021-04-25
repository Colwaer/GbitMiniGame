﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            var t = GetComponentsInChildren<MoveableSting>();
            foreach(MoveableSting item in t)
            {
                item.StartMove();
            }
        }    
    }
}
