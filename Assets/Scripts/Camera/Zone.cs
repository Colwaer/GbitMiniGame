using Cinemachine;
using UnityEngine;

//关卡的一个区域
public class Zone : MonoBehaviour
{
    private CCameraController CameraController;
    private PolygonCollider2D m_PolygonCollider;

    private void Awake()
    {
        CameraController = Camera.main.GetComponentInChildren<CCameraController>();
        m_PolygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraPoint obj = collision.GetComponent<CameraPoint>();
        if(obj!=null)
        {
            CameraController.ChangeConfiner(m_PolygonCollider);
        }
            
    }
}
