using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Renderer m_Renderer;
    public int Index;
    [SerializeField]
    private bool _Picked;
    public bool Picked
    {
        get
        {
            return _Picked;
        }
        set
        {
            m_Renderer.enabled = !value;
            _Picked = value;
        }
    }

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
        if(Picked)
        {
            Debug.Log(Index);
            GameManager.Instance.SetStar(Index, true);
        } 
    }
}
