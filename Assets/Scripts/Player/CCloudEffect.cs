using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCloudEffect : MonoBehaviour
{
    public ParticleSystem CollideEffect;

    private void OnEnable()
    {
        CEventSystem.Instance.CollideCloud += OnCollideCloud;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.CollideCloud -= OnCollideCloud;
    }

    private void OnCollideCloud()
    {
        CollideEffect.Play();
    }
}
