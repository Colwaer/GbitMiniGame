using UnityEngine;

public class CollideEffect : EffectOnPlayer
{
    private ParticleSystem ParticleSystem;

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        CEventSystem.Instance.CollideCloud += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.CollideCloud -= PlayEffect;
    }

    protected override void Effect()
    {
        ParticleSystem.Play();
    }
}
