using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public GameObject wind;
    private int m_Count = 0;
    private bool isSimulated = false;
    public int Count
    {
        get
        {
            return m_Count;
        }
        set
        {
            m_Count = value;
            if (m_Count == Trigger_Count)
            {
                wind.SetActive(true);
                isSimulated = true;
            }
        }
    }
    public int Trigger_Count = 5;

    private void Awake() 
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isSimulated)
            return;
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.gravityScale = -2f;
        }    
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!isSimulated)
            return;
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.gravityScale = -2f;
        }    
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!isSimulated)
            return;
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.gravityScale = 1f;
        } 
    }
}
