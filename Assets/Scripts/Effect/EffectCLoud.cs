﻿using System.Collections;
using UnityEngine;

public class EffectCLoud : MonoBehaviour
{
    private float t_Fade = 1f;
    private float timer;
    private SpriteRenderer m_Renderer;
    private Color DefaultColor;
    [SerializeField] 
    private Color TargetColor;
    private Vector3 DefaultScale;
    [SerializeField] 
    private Vector3 TargetScale;
    private float TargetAndle = 90f;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        DefaultColor = m_Renderer.color;
        DefaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        m_Renderer.color = DefaultColor;
        transform.localScale = DefaultScale;
        transform.eulerAngles = Vector3.zero;
        if (PlayerController.Instance != null)
            PlayerController.Instance.FollowPlayer(transform);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        timer = 0f;
        float percent = 0f;
        for (; percent < 1f; timer += Time.deltaTime)
        {
            percent = timer / t_Fade;
            m_Renderer.color = Color.Lerp(DefaultColor, TargetColor, percent);
            transform.localScale = Vector3.Lerp(DefaultScale, TargetScale, percent);
            transform.eulerAngles = new Vector3(0f, 0f, percent * TargetAndle);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
