using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundElement : MonoBehaviour
{
    public float deep = 1.0f;
    public Vector3 originScreenPosition;
    private Camera mainCamera;
    private Vector3 originCameraPosition;
    [SerializeField] private Vector3 moveOffset;
    
    private void Start() 
    {
        originCameraPosition = Camera.main.transform.position;
        mainCamera = Camera.main;
        transform.position = mainCamera.ScreenToWorldPoint(originScreenPosition);

        
    }
    private void FixedUpdate()
    {
        moveOffset = mainCamera.transform.position - originCameraPosition;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(originScreenPosition + moveOffset * deep);
        transform.position = targetPosition;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
