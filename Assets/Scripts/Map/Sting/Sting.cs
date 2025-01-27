﻿using UnityEngine;

public class Sting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Public.IPlayer player = collision.GetComponent<Public.IPlayer>();
        if(player!=null)
        {
            CAudioController.Instance.PlaySound(ESound.Die);
            player.Die();
        }
    }
}
