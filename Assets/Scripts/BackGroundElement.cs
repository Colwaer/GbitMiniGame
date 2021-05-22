using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundElement : MonoBehaviour
{
    public float deep = 1.0f;
    public Vector3 originScreenPosition;
    new private Camera camera;
    private Vector3 originCameraPosition;
    private Vector3 originTransformPosition;
    private Vector3 moveOffset;
    
    private void Start() 
    {
        originCameraPosition = Camera.main.transform.position;
        camera = Camera.main;
        transform.position = camera.ScreenToWorldPoint(originScreenPosition);
        originTransformPosition = transform.position;

        
    }
    private void FixedUpdate()
    {
            Debug.Log(moveOffset);
            moveOffset = camera.transform.position - originCameraPosition;
            Vector3 targetPosition = camera.ScreenToWorldPoint(originScreenPosition + moveOffset * deep);
            transform.position = targetPosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
