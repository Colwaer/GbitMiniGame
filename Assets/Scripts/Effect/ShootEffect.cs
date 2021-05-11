using UnityEngine;

public class ShootEffect : EffectOnPlayer
{
    protected override void PlayEffect()
    {
        transform.eulerAngles = new Vector3(0, 0, -Public.CTool.Direction2Angle(PlayerController.Instance.Direction) - 90f);
        transform.position = PlayerController.Instance.m_Player.transform.position;
        Effect.Play();
    }
    private void OnEnable()
    {
        CEventSystem.Instance.PlayerShoot += PlayEffect;
    }
    private void OnDisable()
    {
        CEventSystem.Instance.PlayerShoot -= PlayEffect;
    }

}
