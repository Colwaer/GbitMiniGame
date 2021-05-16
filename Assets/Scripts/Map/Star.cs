using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Renderer m_Renderer;
    public int Index;

    private bool _Picked;
    public bool Picked
    {
        get
        {
            return _Picked;
        }
        set
        {
            if(value)
            {
                if(!_Picked)
                    PlayerController.Instance.m_Player.Point += m_Point;
                m_Renderer.enabled = false;
            }
           else
            {
                if(_Picked)
                    PlayerController.Instance.m_Player.Point -= m_Point;
                m_Renderer.enabled = true;
            }
            _Picked = value;
        }
    }
    private int m_Point = 1;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
        if (GameManager.Instance.Stars_Destroyed[Index])
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += OnPlayerDie;
        CEventSystem.Instance.ScenePassed += OnScenePassed;
    }

    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= OnPlayerDie;
        CEventSystem.Instance.ScenePassed -= OnScenePassed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Picked )
        {
            Picked = true;
        }
    }

    private void OnPlayerDie()
    {
        Picked = false;
    }
    private void OnScenePassed()
    {
        GameManager.Instance.SetStar(Index, Picked);
    }
}
