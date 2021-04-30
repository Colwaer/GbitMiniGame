using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

//默认状态下始终跟随玩家
public class CameraController : MonoBehaviour
{
    internal CinemachineVirtualCamera cvc;
    // private float t_Normalize = 2f;  //从任何状态恢复正常需要的时间
    private float m_DefaultSize = 9.0f;   //相机默认大小     
    // private Coroutine ActiveCoroutine = null;
    private Transform transform_MainCamera;
    private Vector2 m_defaultShiftSpeed;
    const float tranformScaleToCameraSize = 2.8f;

    private Transform player;
    private void Awake()
    {
        transform_MainCamera = Camera.main.transform;
        cvc = GetComponent<CinemachineVirtualCamera>();
        m_DefaultSize = cvc.m_Lens.OrthographicSize;
        player = PlayerController.Instance.Player.transform;
        cvc.Follow = player;
        m_defaultShiftSpeed = cvc.m_Lens.LensShift;
        // Debug.Log("cvc follow camera :" + cvc.Follow);
    }

    internal void StartChangeScale(float t_Effect, Transform cameraArea)
    {
        
        StopAllCoroutines();
        
        float targetSize = cameraArea.localScale.x / tranformScaleToCameraSize;
        StartCoroutine(ChangeScale(targetSize, cameraArea, t_Effect));
    }
    IEnumerator ChangeScale(float targetSize, Transform cameraArea, float time)
    {
        cvc.m_Lens.LensShift = m_defaultShiftSpeed / 5;
        cvc.Follow = cameraArea;
        float value = cvc.m_Lens.OrthographicSize;
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            
            Debug.Log(cvc.m_Lens.OrthographicSize);
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(value, targetSize, Mathf.Sqrt(timer / time));
            yield return null;
        }
        cvc.m_Lens.OrthographicSize = targetSize;    
        cvc.m_Lens.LensShift = m_defaultShiftSpeed;
    }
    internal void FollowPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeScale(m_DefaultSize, player, 0.5f));
        cvc.Follow = player;
    }
}
