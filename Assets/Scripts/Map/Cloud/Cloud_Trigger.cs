﻿using UnityEngine;

public class Cloud_Trigger : Cloud
{
    protected override void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            var t = GetComponentsInChildren<Sting_Movable>();
            foreach(Sting_Movable item in t)
            {
                item.StartMove();
            }
        }
        base.OnCollisionEnter2D(other);
    }
}
