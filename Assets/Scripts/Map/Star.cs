﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Renderer m_Renderer;
    public ParticleSystem m_ParticleSystem;

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
        CEventSystem.Instance.CheckPointChanged += CollectStar;
        CEventSystem.Instance.ScenePassed += CollectStar;
    }

    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= OnPlayerDie;
        CEventSystem.Instance.CheckPointChanged -= CollectStar;
        CEventSystem.Instance.ScenePassed -= CollectStar;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !Picked )
        {
            CAudioController.Instance.PlaySound(ESound.GetStar);
            m_ParticleSystem.Play();

            Picked = true;
        }
    }

    private void OnPlayerDie()
    {
        Picked = false;
    }
    private void CollectStar()
    {
        if(Picked)
        {
            //Debug.Log(Index);
            GameManager.Instance.UpdateStar(Index, true);
            Destroy(gameObject);
        } 
    }
}
