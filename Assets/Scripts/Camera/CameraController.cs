using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

//默认状态下始终跟随玩家
public class CameraController : MonoBehaviour
{
    internal CinemachineVirtualCamera cvc;
    private float t_Normalize = 1f;         //恢复跟随玩家需要的时间
    private float m_DefaultSize = 9.0f;     //相机默认大小     
    private Vector2 m_defaultShiftSpeed;
    private const float ScaleToCameraSize = 0.28f;  //下面改成了乘法
    private Transform transform_Player;

    private void Awake()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        m_DefaultSize = cvc.m_Lens.OrthographicSize;
        transform_Player = PlayerController.Instance.Player.transform;
        cvc.Follow = transform_Player;
        m_defaultShiftSpeed = cvc.m_Lens.LensShift;
    }

    internal void StartChangeScale(float t_Effect, Transform cameraArea)
    {
        StopAllCoroutines();
        float targetSize = cameraArea.localScale.x * ScaleToCameraSize;
        StartCoroutine(ChangeScale(targetSize, cameraArea, t_Effect));
    }
    IEnumerator ChangeScale(float targetSize, Transform cameraArea, float time)
    {
        cvc.m_Lens.LensShift = m_defaultShiftSpeed / 5;
        cvc.Follow = cameraArea;
        float value = cvc.m_Lens.OrthographicSize;
        
        for (float timer = 0; timer <= time; timer += Time.deltaTime)
        {
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(value, targetSize, Mathf.Sqrt(timer / time));
            yield return null;
        }
        cvc.m_Lens.OrthographicSize = targetSize;    
        cvc.m_Lens.LensShift = m_defaultShiftSpeed;
    }
    internal void FollowPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeScale(m_DefaultSize, transform_Player, t_Normalize));
        cvc.Follow = transform_Player;
    }
}
