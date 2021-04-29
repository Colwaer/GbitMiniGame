using Cinemachine;
using System.Collections;
using UnityEngine;

//默认状态下始终跟随玩家
public class CameraController : MonoBehaviour
{
    internal CinemachineVirtualCamera cvc;
    private float t_Normalize = 2f;  //从任何状态恢复正常需要的时间
    private float m_DefaultSize;     //相机默认大小     
    private Coroutine ActiveCoroutine = null;
    private Transform transform_MainCamera; 
    private void Awake()
    {
        transform_MainCamera = Camera.main.transform;
        cvc = GetComponent<CinemachineVirtualCamera>();
        m_DefaultSize = cvc.m_Lens.OrthographicSize;
        cvc.Follow = PlayerController.Instance.Player.transform;
    }
    //duration为镜头扩大需要的时间
    public void StartChangeScale(float targetSize, float duration, Transform follow = null)
    {
        if (ActiveCoroutine != null) StopCoroutine(ActiveCoroutine);
        ActiveCoroutine = StartCoroutine(ChangeScale(targetSize, duration, follow));
    }

    public void StopCameraEffect()
    {
        StopAllCoroutines();
        ActiveCoroutine = StartCoroutine(ChangeScale(m_DefaultSize, t_Normalize,PlayerController.Instance.Player.transform));
    }

    private IEnumerator ChangeScale(float targetSize, float duration, Transform follow = null)
    {
        cvc.Follow = null;
        float deltaSize;    //每帧改变的尺寸
        Vector3 deltaPos;   //每帧的位移
        deltaSize = (targetSize - cvc.m_Lens.OrthographicSize) / duration * Time.fixedDeltaTime;
        deltaPos = (follow.position - transform_MainCamera.position) / duration * Time.fixedDeltaTime;
        for (float timer = 0; timer < duration; timer += Time.fixedDeltaTime)
        {
            //Debug.Log(cvc.m_Lens.OrthographicSize);
            cvc.m_Lens.OrthographicSize += deltaSize;
            transform_MainCamera.position += deltaPos;
            yield return new WaitForFixedUpdate();
        }
        cvc.m_Lens.OrthographicSize = targetSize;
        cvc.Follow = follow;
    }
}
