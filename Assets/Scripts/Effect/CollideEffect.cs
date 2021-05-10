using UnityEngine;

public class CollideEffect : EffectOnPlayer
{
    private void OnEnable()
    {
        CEventSystem.Instance.CollideCloud += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.CollideCloud -= PlayEffect;
    }

}
