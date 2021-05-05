using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public GameObject Wind;
    private BoxCollider2D m_BoxCollider;
    private int Trigger_Count = 5;
    [SerializeField] private int _Count = 0;
    public int Count
    {
        get
        {
            return _Count;
        }
        set
        {
            _Count = value;
            if (_Count == Trigger_Count)
            {
                Wind.SetActive(true);
                m_BoxCollider.enabled = true;
            }
        }
    }

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_BoxCollider.enabled = false;
    }
    private void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += OnPlayerDie;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= OnPlayerDie;
    }
    private void OnTriggerEnter2D(Collider2D collosion) 
    {
        if (collosion.CompareTag("Player"))
        {
            collosion.attachedRigidbody.gravityScale = -2f;
        }    
    }
    private void OnTriggerStay2D(Collider2D collosion)
    {
        if (collosion.CompareTag("Player"))
        {
            collosion.attachedRigidbody.gravityScale = -2f;
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerController.Instance.m_Player.b_isDashing)
        {
            collision.attachedRigidbody.gravityScale = 1f;
        } 
    }

    private void OnPlayerDie()
    {
        Count = 0;
        Wind.SetActive(false);
        m_BoxCollider.enabled = false;
    }
}
