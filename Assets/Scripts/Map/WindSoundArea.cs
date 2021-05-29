using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Play");
            CAudioController.Instance.PlaySound(ESound.Wind);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CAudioController.Instance.StopSound(ESound.Wind);
        }
    }
}
