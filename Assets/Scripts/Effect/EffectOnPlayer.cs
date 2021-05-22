using UnityEngine;

public class EffectOnPlayer : MonoBehaviour
{
    protected void PlayEffect()
    {
        PlayerController.Instance.FollowPlayer(transform);
        transform.eulerAngles = new Vector3(0, 0, -Public.CTool.Direction2Angle(PlayerController.Instance.Direction));
        Effect();
    }

    protected virtual void Effect()
    {

    }

}
