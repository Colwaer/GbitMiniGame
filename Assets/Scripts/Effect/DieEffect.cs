using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : EffectOnPlayer
{
    [SerializeField] private ParticleSystem m_Effect;
    private void Start()
    {
        m_Effect = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= PlayEffect;
    }
    protected override void Effect()
    {
        m_Effect.Play();
    }
}
