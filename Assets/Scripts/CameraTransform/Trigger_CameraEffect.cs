using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Trigger_CameraEffect : MonoBehaviour
{
    [SerializeField] private Transform CameraArea;  //拍摄范围
    private CameraController Controller;    //相机控制器的脚本
    private float CameraSize;   //拍摄范围对应的摄像机尺寸
    private float t_Effect = 2f;     //实现相机效果需要的时间            
    private void Start() 
    {
        Controller = Camera.main.GetComponentInChildren<CameraController>();
        CameraSize = CameraArea.lossyScale.y / 10;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(CameraSize);
            Controller.StartChangeScale(CameraSize, t_Effect, CameraArea);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Controller.StopCameraEffect();
        }
    }
}
