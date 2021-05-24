using System.Data;
using System.Collections;
using System.Collections.Generic;
using Public;
using UnityEngine;

public class BackGroundObject_Random : MonoBehaviour
{
    [SerializeField] private float Depth;   //深度，挂载此脚本的物体在镜头中的移动速度时一般物体的1/Depth
    [SerializeField]
    private float AlphaRange;
    [SerializeField]
    private float ScaleRange;
    private Camera MainCamera;
    private Vector3 OriginalCameraPosition;
    [SerializeField]
    private Vector3 OriginalPos;
    private Vector3 CameraOffset;
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        //如果不手动设定深度，所有属性会随机分配
        if (Depth == 0)
        {
            Depth = Random_Background.RandomDepth();
            AlphaRange = Random_Background.RandomAlphaRandge();
            ScaleRange = Random_Background.RandomScaleRandge();
            transform.localPosition = OriginalPos = Random_Background.RandomPos();
        }
        else
        {
            OriginalPos = transform.localPosition;
        }
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, (1 - AlphaRange / 75 * Depth) * 0.8f); 
        m_SpriteRenderer.sortingOrder =(int) -Depth;
        transform.localScale -= transform.localScale * ScaleRange * Depth / 20;
        MainCamera = Camera.main;
        OriginalCameraPosition = MainCamera.transform.position;
    }

    private void Update()
    {
        CameraOffset = MainCamera.transform.position - OriginalCameraPosition;
        Vector3 offset = -CameraOffset / Depth + OriginalPos;
        transform.localPosition = offset;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 10);
    }
}
