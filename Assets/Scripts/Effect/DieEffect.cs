using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private void Start()
    {
        effect = GetComponentInChildren<ParticleSystem>();
    }
    private void OnEnable()
    {
        CEventSystem.Instance.PlayerDie += EnableEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerDie -= EnableEffect;
    }
    public void EnableEffect()
    {
        effect.transform.position = PlayerController.Instance.m_Player.transform.position;
        effect.Play();
    }
}
