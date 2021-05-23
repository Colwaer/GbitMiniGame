using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundElement : MonoBehaviour
{
    [SerializeField] private float Depth;
    [SerializeField]
    private float DeltaAlpha = 0.5f;
    [SerializeField]
    private float DeltaScale = 0.5f;
    private Camera MainCamera;
    private Vector3 OriginalCameraPosition;
    private Vector3 OriginalPos;
    private Vector3 CameraOffset;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        MainCamera = Camera.main;
        OriginalPos = transform.localPosition;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (Depth ==0 ) 
            Depth = Random.Range(2, 10);
    }

    private void Start()
    {
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 1 - DeltaAlpha / 75 * Depth);
        m_SpriteRenderer.sortingOrder =(int) -Depth;
        transform.localScale -= transform.localScale * DeltaScale / 10 * Depth;

        OriginalCameraPosition = MainCamera.transform.position;
    }

    private void Update()
    {
        CameraOffset = MainCamera.transform.position - OriginalCameraPosition;
        Vector3 offset = CameraOffset / Depth + OriginalPos;

        transform.localPosition = offset;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 10);
    }
}
