using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : EffectOnPlayer
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
