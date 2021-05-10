using UnityEngine;

public class ShootEffect : EffectOnPlayer
{
    private void OnEnable()
    {
        CEventSystem.Instance.PlayerShoot += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerShoot -= PlayEffect;
    }

}
