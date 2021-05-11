using UnityEngine;

public class EffectOnPlayer : MonoBehaviour
{
    internal ParticleSystem Effect;

    private void Awake()
    {
        Effect = GetComponent<ParticleSystem>();
    }

    protected virtual void PlayEffect()
    {
        //transform.rotation = Quaternion.Euler(0, 0, -Public.CTool.Direction2Angle(PlayerController.Instance.Direction));
        transform.eulerAngles = new Vector3(0, 0, -Public.CTool.Direction2Angle(PlayerController.Instance.Direction));
        transform.position = PlayerController.Instance.m_Player.transform.position;
        Effect.Play();
    }
}
