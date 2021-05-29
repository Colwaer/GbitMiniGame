using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CAudioController.Instance.PlaySound(ESound.GetStar);
        PlayerController.Instance.m_Player.KeyCount++;
        Destroy(gameObject);
    }
}
