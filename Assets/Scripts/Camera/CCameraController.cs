using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

//默认状态下始终跟随玩家
public class CCameraController : MonoBehaviour
{
    private const float ScaleToCameraSize = 0.28f;
    private float t_Normalize = 1f;         //恢复跟随玩家需要的时间
   
    private float DefaultSize;              //默认相机大小     
    private Vector2 DefaultLensShift;       //默认相机速度

    private CinemachineVirtualCamera m_VirtualCamera;
    private CinemachineConfiner m_Confiner;
    private Transform transform_Player;

    private void Awake()
    {
        m_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_Confiner = GetComponent<CinemachineConfiner>();
        DefaultSize = m_VirtualCamera.m_Lens.OrthographicSize;
        transform_Player = PlayerController.Instance.Player.transform;
        m_VirtualCamera.Follow = transform_Player;
        DefaultLensShift = m_VirtualCamera.m_Lens.LensShift;
    }

    public void ChangeConfiner(PolygonCollider2D polygonCollider)
    {
        m_Confiner.m_BoundingShape2D = polygonCollider;
    }

    //以下方法弃用
    internal void StartChangeScale(float t_Effect, Transform cameraArea)
    {
        StopAllCoroutines();
        float targetSize = cameraArea.localScale.x * ScaleToCameraSize;
        StartCoroutine(ChangeScale(targetSize, cameraArea, t_Effect));
    }
    
    private IEnumerator ChangeScale(float targetSize, Transform cameraArea, float time)
    {
        m_VirtualCamera.m_Lens.LensShift = DefaultLensShift / 5;
        m_VirtualCamera.Follow = cameraArea;
        float value = m_VirtualCamera.m_Lens.OrthographicSize;
        
        for (float timer = 0; timer <= time; timer += Time.deltaTime)
        {
            m_VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(value, targetSize, Mathf.Sqrt(timer / time));
            yield return null;
        }
        m_VirtualCamera.m_Lens.OrthographicSize = targetSize;    
        m_VirtualCamera.m_Lens.LensShift = DefaultLensShift;
    }
    internal void FollowPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeScale(DefaultSize, transform_Player, t_Normalize));
        m_VirtualCamera.Follow = transform_Player;
    }
}
