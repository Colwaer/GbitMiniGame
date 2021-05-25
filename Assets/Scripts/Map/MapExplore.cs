using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapExplore : MonoBehaviour
{
    private PlayerController player;
    private CinemachineVirtualCamera cvCamera;
    private CCameraController cameraController;
    private PolygonCollider2D polygonCollider2D;
    private bool isEnabled;
    public float speed = 3f;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        cameraController = Camera.main.GetComponentInChildren<CCameraController>();
        cvCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        player = PlayerController.Instance;
        CEventSystem.Instance.StartExplore += Enable;
        CEventSystem.Instance.EndExplore += Disable;
        
    }
    private void Update() 
    {
        if (!isEnabled)
            return;
        Vector2 des;
        des.x = Input.GetAxis("Horizontal");
        des.y = Input.GetAxis("Vertical");
        transform.position += (Vector3)des * speed * Time.deltaTime;
    }
    public void Enable()
    {
        
        transform.position = player.m_Player.transform.position;
        isEnabled = true;
        cameraController.ChangeConfiner(polygonCollider2D);
        cameraController.b_EnableChangeConfiner = false;
        cvCamera.Follow = transform;
    }
    public void Disable()
    {
        cameraController.b_EnableChangeConfiner = true;
        isEnabled = false;
        cameraController.SwitchToLastConfiner();
        cvCamera.Follow = player.GetComponentInChildren<CameraPoint>().transform;
    }
}
