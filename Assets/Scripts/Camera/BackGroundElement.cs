using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundElement : MonoBehaviour
{
    public float deep = 1.0f;
    public float deltaAlpha = 0.6f;
    public float deltaScale = 0.6f;
    private Camera mainCamera;
    private Vector3 originCameraPosition;
    private Vector3 originPos;
    [SerializeField] private Vector3 moveOffset;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        originPos = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - deltaAlpha / 75 * deep);
        transform.localScale -= transform.localScale * deltaScale / 75 * deep;


        originCameraPosition = Camera.main.transform.position;
        mainCamera = Camera.main;
        transform.position = mainCamera.transform.position;
    }
    private void Update()
    {
        moveOffset = mainCamera.transform.position - originCameraPosition;
        Vector3 offset = moveOffset / deep + originPos;

        transform.localPosition = offset;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 10);
    }
}
