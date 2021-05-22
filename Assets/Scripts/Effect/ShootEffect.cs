using System.Collections;
using UnityEngine;

public class ShootEffect : EffectOnPlayer
{
    [SerializeField] private GameObject EffectCloud;
    private float t_Dash;
    private float Interval = 0.05f;   //产生效果云的时间间隔

    private void OnEnable()
    {
        CEventSystem.Instance.PlayerShoot += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerShoot -= PlayEffect;
    }

    private void Start()
    {
        t_Dash = PlayerController.Instance.m_Player.t_Dash;
    }

    protected override void Effect()
    {
        StartCoroutine(GenerateEffectCloud());
    }

    private IEnumerator GenerateEffectCloud()
    {
        int times = (int)(t_Dash / Interval);
        for (int i = 0; i < times; i++)
        {
            PlayerController.Instance.FollowPlayer(transform);
            Instantiate(EffectCloud, transform.position, Public.CTool.ZeroRotation);
            yield return Public.CTool.Wait(Interval);
        }
    }
}
