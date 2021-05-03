using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

//默认状态下始终跟随玩家
public class CameraController : MonoBehaviour
{
    private const float ScaleToCameraSize = 0.28f;
    private float t_Normalize = 1f;         //恢复跟随玩家需要的时间
   
    private float DefaultSize;              //默认相机大小     
    private Vector2 DefaultLensShift;       //默认相机速度

    private CinemachineVirtualCamera Vcam;
    private Transform transform_Player;

    private void Awake()
    {
        Vcam = GetComponent<CinemachineVirtualCamera>();
        DefaultSize = Vcam.m_Lens.OrthographicSize;
        transform_Player = PlayerController.Instance.Player.transform;
        Vcam.Follow = transform_Player;
        DefaultLensShift = Vcam.m_Lens.LensShift;
    }

    internal void StartChangeScale(float t_Effect, Transform cameraArea)
    {
        StopAllCoroutines();
        float targetSize = cameraArea.localScale.x * ScaleToCameraSize;
        StartCoroutine(ChangeScale(targetSize, cameraArea, t_Effect));
    }
    IEnumerator ChangeScale(float targetSize, Transform cameraArea, float time)
    {
        Vcam.m_Lens.LensShift = DefaultLensShift / 5;
        Vcam.Follow = cameraArea;
        float value = Vcam.m_Lens.OrthographicSize;
        
        for (float timer = 0; timer <= time; timer += Time.deltaTime)
        {
            Vcam.m_Lens.OrthographicSize = Mathf.Lerp(value, targetSize, Mathf.Sqrt(timer / time));
            yield return null;
        }
        Vcam.m_Lens.OrthographicSize = targetSize;    
        Vcam.m_Lens.LensShift = DefaultLensShift;
    }
    internal void FollowPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeScale(DefaultSize, transform_Player, t_Normalize));
        Vcam.Follow = transform_Player;
    }
}
